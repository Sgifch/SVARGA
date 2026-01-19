using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFrog : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private State startingState = State.Idle;

    public float stopDistance = 2f;
    public float activationDistance = 5f; // Дистанция активации по R

    public KeyCode activationKey = KeyCode.R;
    public bool isActive = false; // Включен ли агент

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private State state;

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;

    private Animator animator;
    private bool isMove;
    //private bool isAttack;

    private enum State
    {
        Idle,
        Roaming,
        //Attack
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

        CheckActivation();

        if (!isActive)
        {
            SetState(State.Idle);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (state)
        {
            default:  
            case State.Idle:
                isMove = false;
                break;
            case State.Roaming:
                Roaming();
                if (!isMove)
                {
                    GetComponent<Animator>().SetBool("Roaming", true);
                    isMove = true;
                }
                break;
                //case State.Attack:
                //if (!isAttack)
                //{
                //    animation.SetTrigger("Attack");
                //}
                //break;
        }

        Animated();
    }

    private void CheckActivation()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (Input.GetKeyDown(activationKey) && distanceToPlayer <= activationDistance)
        {
            isActive = !isActive; // Переключаем состояние

            if (isActive)
            {
                SetState(State.Roaming);
            }
        }
    }

    private void Roaming()
    {
        roamPosition = playerTransform.position;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > stopDistance)
        {
            navMeshAgent.SetDestination(roamPosition);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }

        vectorRoaming = navMeshAgent.velocity; 
        vectorRoaming.Normalize();
    }

    private void Animated()
    {
        GetComponent<Animator>().SetFloat("Horizontal", vectorRoaming.x);
        GetComponent<Animator>().SetFloat("Vertical", vectorRoaming.y);
    }

    private void SetState(State newState)
    {
        state = newState;
    }

     public void SetActive(bool active)
    {
        isActive = active;
        
        if (isActive)
        {
            SetState(State.Roaming);
        }
        else
        {
            SetState(State.Idle);
            navMeshAgent.SetDestination(transform.position);
        }
    }
}

