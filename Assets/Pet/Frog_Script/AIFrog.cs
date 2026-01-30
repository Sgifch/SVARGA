using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFrog : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;
    public Transform currentEnemy;

    //public Rigidbody2D rb;

    public float stopDistance = 1.5f;
    public float activationDistance = 5f; // Дистанция активации по R
    public float attackDistance = 0.5f;

    public float AttackInterval =1f;
    public float AttackTimer =0f;

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

        if (!isActive)
        {
            state = State.Idle;
            Animated();
            return;
        }

        switch (state)
        {
            default:  
            case State.Idle:

                isMove = false;
                animator.SetBool("Roaming", false);
                break;

            case State.Roaming:

                Roaming();
                if (!isMove)
                {
                    animator.SetBool("Roaming", true);
                    isMove = true;
                }
                break;

            case State.Attack:
                if (!isAttack)
                {
                    animator.SetTrigger("Attack");
                    isMove = false;
                    isAttack = true;
                }
                break;
        }

        if (state != State.Attack)
        {
            Roaming();
        }

        AttackTimer += Time.deltaTime;

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

            roamPosition = playerTransform.position;

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > stopDistance)
            {
                navMeshAgent.SetDestination(roamPosition);
                state = State.Roaming;
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                state = State.Idle;
            }
        }
        else if (currentEnemy  != null)
        {
            roamPosition = currentEnemy.position;
            float distanceToEnemy = Vector3.Distance(transform.position, currentEnemy.position);

            if (distanceToEnemy > attackDistance)
            {
                navMeshAgent.SetDestination(roamPosition);
                state = State.Roaming;
            }
            else if (distanceToEnemy <= attackDistance)
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
            GetComponent<Animator>().SetTrigger("Attack");
            animator.SetBool("Roaming", false);
        }
        // Если двигаемся, используем вектор движения
        else if (vectorRoaming != Vector3.zero && vectorRoaming.magnitude > 0.1f)
        {
            directionForAnimation = vectorRoaming;
            GetComponent<Animator>().SetTrigger("Roaming");
        }
        // Иначе всегда смотрим на игрока
        else
        {
            directionForAnimation = directionToPlayer;
        }

        animator.SetFloat("Horizontal", directionForAnimation.x);
        animator.SetFloat("Vertical", directionForAnimation.y);
    }

}

