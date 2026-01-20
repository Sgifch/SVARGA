using GameLogic.Utilits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAI : MonoBehaviour
{
    [SerializeField] private State startingState;

    public GameObject visualComponent; //Потом переделать под гетКомпонент

    public float attackDistance = 5f; //Расстояние для атаки
    public float retreatDistance = 2f; //Расстояние для отбегания
    private float currentDistance;

    private NavMeshAgent navMeshAgent;
    public State state; //Текущие состояние агента
    private Vector3 roamPosition; //Новая точка движения
    private Vector3 startingPosition;
    private Vector3 vectorRoaming;

    private Vector3 targetLook;

    private Animator animation;
    private bool isMove;

    public GameObject followObject;
    public enum State
    {
        Idle,
        Roaming,
        Attack
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; //Отключение вращение
        navMeshAgent.updateUpAxis = false; //Отключение влияния ориентации???
        state = startingState;
    }

    private void Start()
    {
        startingPosition = transform.position;
        if (visualComponent != null)
        {
            animation = visualComponent.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        switch (state)
        {
            default:  //Если ничего не задано значит по умолчанию будет состояние Idle
            case State.Idle:
                break;

            case State.Roaming:
                Roaming();
                if (!isMove)
                {
                    animation.SetTrigger("Roaming");
                    isMove = true;
                }
                break;
            case State.Attack:
                break;
        }

        if(state != State.Attack)
        {
            Animated();
        }
    }

    private void Roaming()
    {
        currentDistance = Vector3.Distance(transform.position, followObject.transform.position);
        //print(currentDistance);

        if (currentDistance <= retreatDistance)
        {
            roamPosition = Retreat();
        }
        else if(currentDistance >= attackDistance)
        {
            //Разворот в сторону игрока
            //navMeshAgent.isStopped = true;
            if (state != State.Attack)
            {
                targetLook = (followObject.transform.position).normalized;
                print(targetLook);
                animation.SetFloat("MoveX", targetLook.x);
                animation.SetFloat("MoveY", targetLook.y);
                state = State.Attack;
            }
        }

        navMeshAgent.SetDestination(roamPosition); //Новая точка для движения

        vectorRoaming = navMeshAgent.velocity; //Фактический вектор скорости
        vectorRoaming.Normalize();
    }

    private void Animated()
    {
        animation.SetFloat("MoveX", vectorRoaming.x);
        animation.SetFloat("MoveY", vectorRoaming.y);
    }

    private Vector3 Retreat()
    {
        Vector3 directionToPlayer = transform.position - followObject.transform.position; //Направление от игрока
        Vector3 retreatTarget = transform.position + directionToPlayer.normalized * attackDistance;

        //Чтобы враг не пытался выбежать за пределы NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatTarget, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return retreatTarget;
    }

    private void Attack()
    {
        state = State.Attack;
    }
}
