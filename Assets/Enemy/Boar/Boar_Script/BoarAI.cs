using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAI : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;

    public float attackDistance;
    public float staggerDuration = 10f;

    public float time = 0f;
    public float interval = 2f;

    private float staggerTimer = 0f;
    private bool wasInAttackRange = false; // Был ли игрок в зоне атаки

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition;
    private Vector3 vectorRoaming;
    private Vector3 directionToPlayer;

    private Animator animator;
    private bool isMove;
    private bool isAttack;

    public enum State
    {
        Idle,
        Roaming,
        Attack,
        Stagger
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateDirectionToPlayer();

        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (staggerTimer > 0)
        {
            staggerTimer -= Time.deltaTime;
            if (staggerTimer <= 0 && state == State.Stagger)
            {
                state = State.Idle;
            }
        }

        switch (state)
        {
            default:
            case State.Idle:
                isMove = false;
                isAttack = false;
                navMeshAgent.isStopped = true;
                break;

            case State.Roaming:
                if (!isMove)
                {
                    isMove = true;
                }
                navMeshAgent.isStopped = false;
                break;

            case State.Attack:
                if (!isAttack)
                {
                    isMove = false;
                    isAttack = true;
                }
                navMeshAgent.isStopped = true;
                break;

            case State.Stagger:
                isMove = false;
                isAttack = false;
                navMeshAgent.isStopped = true;
                break;
        }

        Roaming();
        Animated();
    }

    private void Roaming()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool isInAttackRange = distanceToPlayer <= attackDistance;

        // Если в состоянии отдыха - не двигаемся
        if (state == State.Stagger)
        {
            navMeshAgent.SetDestination(transform.position);
            return;
        }

        // Если игрок в зоне атаки
        if (isInAttackRange)
        {
            wasInAttackRange = true;
            navMeshAgent.SetDestination(transform.position);

            // Атакуем если прошел кулдаун
            if (time <= 0)
            {
                state = State.Attack;
                time = interval;
            }
            else
            {
                state = State.Idle;
            }
        }
        else // Игрок вне зоны атаки
        {
            // Если только что вышел из зоны атаки - включаем отдых
            if (wasInAttackRange)
            {
                wasInAttackRange = false;
                staggerTimer = staggerDuration;
                state = State.Stagger;
            }
            else if (state != State.Stagger)
            {
                // Преследуем игрока
                roamPosition = playerTransform.position;
                navMeshAgent.SetDestination(roamPosition);
                state = State.Roaming;
            }
        }

        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            vectorRoaming = navMeshAgent.velocity;
            vectorRoaming.Normalize();
        }
        else
        {
            vectorRoaming = Vector3.zero;
        }
    }

    private void UpdateDirectionToPlayer()
    {
        if (playerTransform != null)
        {
            directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize();
        }
    }

    private void Animated()
    {
        if (animator == null) return;

        Vector3 directionForAnimation;

        if (state == State.Attack)
        {
            directionForAnimation = directionToPlayer;
            animator.SetTrigger("Attack");
            animator.SetBool("Roaming", false);
        }
        else if (state == State.Stagger)
        {
            directionForAnimation = directionToPlayer;
            animator.SetBool("Roaming", false);
        }
        else if (vectorRoaming != Vector3.zero && vectorRoaming.magnitude > 0.1f)
        {
            directionForAnimation = vectorRoaming;
            animator.SetBool("Roaming", true);
        }
        else
        {
            directionForAnimation = directionToPlayer;
            animator.SetBool("Roaming", false);
        }

        animator.SetFloat("Horizontal", directionForAnimation.x);
        animator.SetFloat("Vertical", directionForAnimation.y);
    }
}
