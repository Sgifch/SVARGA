using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAI : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;

    public GameObject visualComponent;

    public float attackDistance;
    public float restDuration = 10f; // Время отдыха после выхода из зоны атаки

    public float time = 0f;
    public float interval = 2f;

    private float restTimer = 0f; // Таймер отдыха
    private bool wasInAttackRange = false; // Был ли игрок в зоне атаки

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition;
    private Vector3 vectorRoaming;
    private Vector3 directionToPlayer;

    private Animator animator;
    private bool isMove;
    private bool isAttack;
    private float originalAnimationSpeed;

    public enum State
    {
        Idle,
        Roaming,
        Attack
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
        if (animator != null)
        {
            originalAnimationSpeed = animator.speed;
        }
    }

    void Update()
    {
        UpdateDirectionToPlayer();

        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (restTimer > 0)
        {
            restTimer -= Time.deltaTime;
        }

        switch (state)
        {
            default:
            case State.Idle:
                isMove = false;
                isAttack = false;
                navMeshAgent.isStopped = true;
                animator.speed = originalAnimationSpeed;
                break;

            case State.Roaming:
                if (!isMove)
                {
                    isMove = true;
                }
                navMeshAgent.isStopped = false;
                animator.speed = originalAnimationSpeed * 2f;
                break;

            case State.Attack:
                if (!isAttack)
                {
                    isMove = false;
                    isAttack = true;
                }
                navMeshAgent.isStopped = true;
                animator.speed = originalAnimationSpeed;
                break;
        }

        Roaming();
        Animated();
    }

    private void Roaming()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool isInAttackRange = distanceToPlayer <= attackDistance;

        // Если идет отдых - стоим на месте
        if (restTimer > 0)
        {
            navMeshAgent.SetDestination(transform.position);
            state = State.Idle;
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
                restTimer = restDuration;
                state = State.Idle;
            }
            else
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
