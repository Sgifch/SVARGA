using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameLogic.Utilits;
using UnityEngine.Tilemaps;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private State startingState;

    public GameObject visualComponent; //Потом переделать под гетКомпонент

    public float roamingDistanceMax = 7f; //Максимальное расстояние на которое уходит объект
    public float roamingDistanceMin = 3f; //Максимальное расстояние на которое уходит объект
    public float roamingTimerMax = 2f; //Время в течение которрого объект двигается

    private NavMeshAgent navMeshAgent;
    private State state; //Текущие состояние агента
    private float roamingTime; //Текущие время roamoing
    private Vector3 roamPosition; //Новая точка движения
    private Vector3 startingPosition;
    private Vector3 vectorRoaming;

    private Animator animation;

    private enum State
    {
        Idle,
        Roaming
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
        startingPosition = transform.position;
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
                break;

            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }

        Animated();
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();

        navMeshAgent.SetDestination(roamPosition); //Новая точка для движения
    }

    private Vector3 GetRoamingPosition()
    {
        vectorRoaming = GameUtilits.GetRandomDir();
        return startingPosition + vectorRoaming * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void Animated()
    {
        animation.SetTrigger("Roaming");
        animation.SetFloat("MoveX", vectorRoaming.x);
        animation.SetFloat("MoveY", vectorRoaming.y);
    }
}
