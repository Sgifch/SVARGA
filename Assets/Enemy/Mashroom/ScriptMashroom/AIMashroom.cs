using GameLogic.Utilits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMashroom : MonoBehaviour
{
    [SerializeField] private State startingState;

    public EnemyScriptableObject enemyProfile;

    public GameObject visualComponent; //Потом переделать под гетКомпонент
    public GameObject triggerComponent;

    private NavMeshAgent navMeshAgent;
    public State state; //Текущие состояние агента

    private Vector3 roamPosition; //Новая точка движения
    private Vector3 vectorRoaming;

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
                Roaming();
                if (!isMove)
                {
                    animation.SetBool("Roaming", true);
                    isMove = true;
                }
                break;
            case State.Attack:
                if (!isAttack)
                {
                    animation.SetTrigger("Attack");
                }
                break;
        }

        Animated();
    }

    private void Roaming()
    {
        roamPosition = followObject.transform.position;

        navMeshAgent.SetDestination(roamPosition); //Новая точка для движения

        vectorRoaming = navMeshAgent.velocity; //Фактический вектор скорости
        vectorRoaming.Normalize();
    }

    public void Attack()
    {
        if (triggerComponent.GetComponent<TriggerMashroom>().isTrigger)
        {
            GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().Damage(enemyProfile.attackPoint);
        }

        gameObject.GetComponent<EnemyControllHealthPoint>().Dead();
    }

    private void Animated()
    {
        animation.SetFloat("MoveX", vectorRoaming.x);
        animation.SetFloat("MoveY", vectorRoaming.y);
    }
}
