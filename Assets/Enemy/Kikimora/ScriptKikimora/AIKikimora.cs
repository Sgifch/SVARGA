using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIKikimora : MonoBehaviour
{
    [SerializeField] private State startingState;

    public EnemyScriptableObject enemyProfile;

    public GameObject visualComponent; //Потом переделать под гетКомпонент
    public GameObject triggerComponent;

    private NavMeshAgent navMeshAgent;
    public State state; //Текущие состояние агента

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;
    private float currentDistance;
    public float attackDistance;

    private Animator animation;
    private bool isMove;
    private bool isAttack;
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
                isMove = false;
                break;

            case State.Roaming:
                navMeshAgent.isStopped = false;
                if (!isMove)
                {
                    isMove = true;
                }
                break;
            case State.Attack:
                navMeshAgent.isStopped = true;
                if (!isAttack)
                {
                    animation.SetTrigger("Attack");
                }
                break;
        }

        if (followObject != null && state != State.Attack)
        {
            Roaming();
        }

        if (state != State.Attack)
        {
            Animated();
        }

    }

    private void Roaming()
    {
        currentDistance = Vector3.Distance(transform.position, followObject.transform.position);

        if (currentDistance > attackDistance)
        {
            state = State.Roaming;
            roamPosition = followObject.transform.position;
            navMeshAgent.SetDestination(roamPosition); //Новая точка для движения

            vectorRoaming = navMeshAgent.velocity; //Фактический вектор скорости
            vectorRoaming.Normalize();
        }
        else if (currentDistance <= attackDistance)
        {
            state = State.Attack;
            animation.SetTrigger("Attack");
        }
    }

    public void Attack()
    {

    }

    private void Animated()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animation.SetTrigger("Roaming");
            animation.SetFloat("MoveX", vectorRoaming.x);
            animation.SetFloat("MoveY", vectorRoaming.y);
        }
        else
        {
            animation.SetTrigger("Idle");
        }
    }
}
