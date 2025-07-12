using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameLogic.Utilits;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private State startingState;

    public float roamingDistanceMax = 7f; //Максимальное расстояние на которое уходит объект
    public float roamingDistanceMin = 3f; //Максимальное расстояние на которое уходит объект
    public float roamingTimerMax = 2f; //Время в течение которрого объект двигается

    private NavMeshAgent navMeshAgent;
    private State state; //Текущие состояние агента
    private float roamingTime; //Текущие время roamoing
    private Vector3 roamPosition; //Новая точка движения
    private Vector3 startingPosition;

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
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition); //Новая точка для движения
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GameUtilits.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
