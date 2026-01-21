using GameLogic.Utilits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAI : MonoBehaviour
{
    [SerializeField] private State startingState;

    public GameObject visualComponent;

    public float attackDistance = 5f; //Расстояние для атаки
    public float retreatDistance = 2f; //Расстояние для отбегания
    private float currentDistance;

    public float interval;
    private float time = 0;

    private NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition;
    private Vector3 vectorRoaming;
    private Vector3 targetLook;

    private Animator animation;

    public bool isMove;
    public bool isAttack;
    public bool isIdle;

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
        navMeshAgent.updateUpAxis = false; //Отключение влияния ориентации
        state = startingState;
    }

    private void Start()
    {
        if (visualComponent != null)
        {
            animation = visualComponent.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                if (followObject != null)
                {
                    state = State.Roaming;
                }

                if (!isIdle)
                {
                    isIdle = true;
                    isMove = false;
                    isAttack = false;
                }
                break;

            case State.Roaming:
                navMeshAgent.isStopped = false;
                Roaming();
                if (!isMove)
                {
                    animation.SetTrigger("Roaming");
                    isMove = true;
                    isIdle = false;
                    isAttack = false;
                }
                break;

            case State.Attack:
                
                animation.SetTrigger("Attack");
                isMove = false;
                isIdle = false;
                isAttack = true;
                break;
        }

        time += Time.deltaTime;

        if(state != State.Attack)
        {
            Animated();
        }
    }

    private void Roaming()
    {
        currentDistance = Vector3.Distance(transform.position, followObject.transform.position);
        //print(currentDistance);

        if (currentDistance <= attackDistance)
        {
            roamPosition = Retreat();
        }
        else if(currentDistance >= attackDistance)
        {
            //Разворот в сторону игрока
            navMeshAgent.isStopped = true;
            if (state != State.Attack && time>=interval)
            {
                targetLook = (followObject.transform.position).normalized;
                print(targetLook);
                animation.SetFloat("MoveX", targetLook.x);
                animation.SetFloat("MoveY", targetLook.y);
                state = State.Attack;
                
                time = 0;
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
        Vector3 retreatTarget = transform.position + directionToPlayer.normalized;

        //Чтобы враг не пытался выбежать за пределы NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatTarget, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return retreatTarget;
    }

    public void AttackFinished()
    {
        state = State.Roaming;
    }
}
