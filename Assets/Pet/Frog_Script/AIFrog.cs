using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFrog : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;
    public Transform currentEnemy;

    public float stopDistance;
    public float activationDistance; // Дистанция активации по R
    public float attackDistance;

    public KeyCode activationKey = KeyCode.R;
    public bool isActive = false; // Активация агента

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;
    private Vector3 directionToPlayer; //Направление к игроку
    private Vector3 directionToEnemy; //Направление к врагу

    private Animator animator;
    private bool isMove;
    private bool isAttack;

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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy != null)
        {
            UpdateDirectionToEnemy();
        }
        else
        {
            UpdateDirectionToPlayer();
        }

        CheckActivation();

        switch (state)
        {
            default:  
            case State.Idle:
                isMove = false;
                isAttack = false;
                break;

            case State.Roaming:
                if (!isMove)
                {
                    isMove = true;
                }
                break;

            case State.Attack:
                if (!isAttack)
                {
                    isMove = false;
                    isAttack = true;
                }
                break;
        }

        if (isActive)
        {
            Roaming();
        }

        Animated();
    }

    private void CheckActivation()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (Input.GetKeyDown(activationKey) && distanceToPlayer <= activationDistance)
        {
            isActive = !isActive; // Переключаем состояние

            if (!isActive)
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }
    }

    private void Roaming()
    {
        if (currentEnemy == null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > stopDistance)
            {
                roamPosition = playerTransform.position;
                navMeshAgent.SetDestination(roamPosition);
                state = State.Roaming;
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                state = State.Idle;
            }
        }
        else
        {
            float distanceToEnemy = Vector3.Distance(transform.position, currentEnemy.position);

            if (distanceToEnemy > attackDistance)
            {
                roamPosition = currentEnemy.position;
                navMeshAgent.SetDestination(roamPosition);
                state = State.Roaming;
            }
            else 
            {
                navMeshAgent.SetDestination(transform.position);
                state = State.Attack;
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

    public void UpdateDirectionToEnemy()
    {
        if (currentEnemy != null)
        {
            directionToEnemy = currentEnemy.transform.position - transform.position;
            directionToEnemy.Normalize();
        }
    }

    private void Animated()
    {
        Vector3 directionForAnimation;

        if (state == State.Attack && currentEnemy != null)
        {
            directionForAnimation = directionToEnemy;
            animator.SetTrigger("Attack");
            animator.SetBool("Roaming", false);
        }
        // Если двигаемся, используем вектор движения
        else if (vectorRoaming != Vector3.zero && vectorRoaming.magnitude > 0.1f)
        {
            directionForAnimation = vectorRoaming;
            animator.SetBool("Roaming", true);
        }
        // Иначе всегда смотрим на игрока
        else
        {
            directionForAnimation = directionToPlayer;
            animator.SetBool("Roaming", false);
        }

        animator.SetFloat("Horizontal", directionForAnimation.x);
        animator.SetFloat("Vertical", directionForAnimation.y);
    }

    private void OnDrawGizmosSelected() // Проверка дистанции от игрока и врага
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }

}

