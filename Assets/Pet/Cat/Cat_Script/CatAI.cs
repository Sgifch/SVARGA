using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;
    public Transform currentEnemy;

    public float stopDistance;
    public float activationDistance; // Дистанция активации по R
    public float attackDistance;

    public KeyCode activationKey = KeyCode.F;
    public bool isActive = false; // Активация агента

    [Header("Teleport Settings")]
    [SerializeField] private float teleportMaxDistance = 15f;
    [SerializeField] private float teleportOffset = 3f;

    public float time = 0f;    // Текущее время кулдауна
    public float interval = 2f; // Интервал между атаками

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;
    private Vector3 directionToPlayer; //Направление к игроку
    private Vector3 directionToEnemy; //Направление к врагу

    private Animator animator;
    private bool isMove;
    private bool isAttack;

    private Vector3 lastPlayerPosition; // Последняя позиция игрока для отслеживания телепортации

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
        animator = GetComponentInChildren<Animator>();

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogWarning("Объкт с тегом 'Player' не найден на сцене!");
            }
        }
        else
        {
            lastPlayerPosition = playerTransform.position;
        }
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
        CheckPlayerTeleport();

        // Обновление кулдауна
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

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

    private void CheckPlayerTeleport()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(playerTransform.position, lastPlayerPosition);

        // Если игрок телепортировался на большое расстояние (сменил комнату)
        if (distance > teleportMaxDistance && isActive)
        {
            // Телепортируем лягушку к игроку с отступом
            Vector3 teleportPosition = playerTransform.position + (transform.position - lastPlayerPosition).normalized * teleportOffset;

            // Ограничиваем расстояние телепортации
            if (Vector3.Distance(playerTransform.position, teleportPosition) > teleportMaxDistance)
            {
                teleportPosition = playerTransform.position - (playerTransform.position - teleportPosition).normalized * teleportMaxDistance;
            }

            navMeshAgent.Warp(teleportPosition);
        }

        lastPlayerPosition = playerTransform.position;
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

                // Проверка кулдауна
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
}
