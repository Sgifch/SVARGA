using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameLogic.Utilits;
using UnityEngine.Tilemaps;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private State startingState;

    public GameObject visualComponent; //����� ���������� ��� ������������

    public float roamingDistanceMax = 7f; //������������ ���������� �� ������� ������ ������
    public float roamingDistanceMin = 3f; //������������ ���������� �� ������� ������ ������
    public float roamingTimerMax = 2f; //����� � ������� ��������� ������ ���������

    private NavMeshAgent navMeshAgent;
    private State state; //������� ��������� ������
    private float roamingTime; //������� ����� roamoing
    private Vector3 roamPosition; //����� ����� ��������
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
        navMeshAgent.updateRotation = false; //���������� ��������
        navMeshAgent.updateUpAxis = false; //���������� ������� ����������???
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
            default:  //���� ������ �� ������ ������ �� ��������� ����� ��������� Idle
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

        navMeshAgent.SetDestination(roamPosition); //����� ����� ��� ��������
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
