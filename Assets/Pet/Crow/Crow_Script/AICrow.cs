using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICrow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public State startingState = State.Idle;

    public float stopDistance;
    public float activationDistance; // Дистанция активации по R

    public KeyCode activationKey = KeyCode.R;
    public bool isActive = false; // Активация агента

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public State state;

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;
    private Vector3 directionToPlayer; //Направление к игроку

    private Animator animator;
    private bool isMove;

    public enum State
    {
        Idle,
        Roaming
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
    }

    // Update is called once per frame
    void Update()
    {

        UpdateDirectionToPlayer();

        CheckActivation();

        switch (state)
        {
            default:
            case State.Idle:
                isMove = false;
                break;

            case State.Roaming:
                if (!isMove)
                {
                    isMove = true;
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
        Vector3 directionForAnimation;

        // Если двигаемся, используем вектор движения
        if (isActive == true)
        {
            directionForAnimation = vectorRoaming;
            if (directionForAnimation == Vector3.zero)
            {
                directionForAnimation = directionToPlayer;
            }
            animator.SetTrigger("TakeOff");
            animator.SetBool("IsFlying", true);
        }
        // Иначе всегда смотрим на игрока
        else
        {
            directionForAnimation = directionToPlayer;
            animator.SetTrigger("Landing");
            animator.SetBool("IsFlying", false);
        }

        animator.SetFloat("Horizontal", directionForAnimation.x);
        animator.SetFloat("Vertical", directionForAnimation.y);
    }
}
