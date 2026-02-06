using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EyesAI : MonoBehaviour
{
    [SerializeField] private State startingState;

    public GameObject visualComponent;

    public float attackDistance = 5f; //Расстояние для атаки
    public float retreatDistance = 2f; //Расстояние для отбегания
    private float currentDistance;

    public float interval;
    public float time;

    private NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition;
    private Vector3 vectorRoaming;
    private Vector3 targetLook;
    public Vector3 lookDirection;

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
        time = interval;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                if (currentDistance <= attackDistance)
                {
                    state = State.Roaming;
                }

                if (!isIdle)
                {
                    //animation.SetTrigger("Idle");
                    isIdle = true;
                    isMove = false;
                    isAttack = false;
                }
                break;

            case State.Roaming:
                //Roaming();
                if (!isMove)
                {
                    //animation.SetTrigger("Roaming");
                    isMove = true;
                    isIdle = false;
                    isAttack = false;
                }
                break;

            case State.Attack:

                //Attack();
                navMeshAgent.isStopped = true;
                if (!isAttack)
                {
                    animation.SetTrigger("Attack");
                    isMove = false;
                    isIdle = false;
                    isAttack = true;
                }

                break;
        }

        if (followObject == null)
        {
            return;
        }

        if (state != State.Attack)
        {
            Roaming();
            currentDistance = Vector3.Distance(transform.position, followObject.transform.position);
        }

        time += Time.deltaTime;

        if (state != State.Attack)
        {
            Animated();
        }

    }

    private void Roaming()
    {
        //currentDistance = Vector3.Distance(transform.position, followObject.transform.position);
        //navMeshAgent.isStopped = false;

        if (currentDistance <= attackDistance)
        {
            state = State.Roaming;
            navMeshAgent.isStopped = false;
            roamPosition = Retreat();
            navMeshAgent.SetDestination(roamPosition); //Новая точка для движения

            vectorRoaming = navMeshAgent.velocity; //Фактический вектор скорости
            vectorRoaming.Normalize();
        }
        else if (currentDistance >= attackDistance)
        {
            if (state != State.Attack && time >= interval)
            {
                targetLook = (followObject.transform.position).normalized;
                Attack();
                state = State.Attack;

                time = 0;
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }

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

    private Vector3 Retreat()
    {
        Vector3 directionAwayFromPlayer = transform.position - followObject.transform.position;
        Vector3 retreatTarget = transform.position + directionAwayFromPlayer.normalized * retreatDistance;

        //Чтобы враг не пытался выбежать за пределы NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatTarget, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    public void Attack()
    {
        //navMeshAgent.isStopped = true;
        lookDirection = (followObject.transform.position - transform.position).normalized;
        animation.SetFloat("MoveX", lookDirection.x);
        animation.SetFloat("MoveY", lookDirection.y);
    }
}
