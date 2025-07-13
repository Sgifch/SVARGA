using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllHealthPoint : MonoBehaviour
{
    public characterStat stat;
    private State stateH = State.Idle; //Состояние здоровья игрока

    public float currentHealthPoint;
    public float currentMannaPoint;

    public Image healthBar;
    public Image mannaBar;

    private int attackPoint;
    private float timeDamage;
    private float intervalTimeDamage;
    private float currentTimeAttack;
    private float currentIntervalTimeAttack;

    private int recoveryPoint;
    private float timeRecovery;
    private float intervalTimeRecovery;
    private float currentTimeRecovery;
    private float currentIntervalTimeRecovery;

    private bool isRecovery = false;

    private enum State
    {
        Idle,
        ContinuosDamage
    }

    //Изменеение урона в bar
    public void ChangeHealthBar() 
    {
        currentHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
        healthBar.fillAmount = currentHealthPoint;
    }

    //Единовременное востановление
    public void Recovery(int _recoveryPoint)
    {
        stat.healthPoint = stat.healthPoint + _recoveryPoint;
        ChangeHealthBar();
    }

    //Полное восстановление
    public void FullRecovery()
    {
        stat.healthPoint = stat.maxHealthPoint;
        ChangeHealthBar();
    }

    //Продолжительное восстановление
    public void TimeRecovery(float _timeRecovery, float _intervalTimeRecovery, int _recoveryPoint)
    {
        recoveryPoint = _recoveryPoint;
        timeRecovery = _timeRecovery;
        intervalTimeRecovery = _intervalTimeRecovery;

        isRecovery = true;
    }

    private void HealthTimeRecovery()
    {
        if (currentTimeRecovery < timeRecovery)
        {
            if (currentIntervalTimeRecovery >= intervalTimeRecovery)
            {
                stat.healthPoint = stat.healthPoint + attackPoint;
                currentIntervalTimeRecovery = 0;
                ChangeHealthBar();
            }

        }
        else
        {
            isRecovery = false;
            recoveryPoint = 0;
            timeRecovery = 0;
            intervalTimeRecovery = 0;
        }

    }

    //Единовременный урон
    public void Damage(int _attackPoint)
    {
        stat.healthPoint = stat.healthPoint - attackPoint;
        ChangeHealthBar();
    }

    //Продолжительный урон
    public void TimeDamage(float _timeDamage, float _intervalTimeDamage, int _attackPoint)
    {
        attackPoint = _attackPoint;
        timeDamage = _timeDamage;
        intervalTimeDamage = _intervalTimeDamage;

        stateH = State.ContinuosDamage;
    }

    private void AttackTimeDamage()
    {
        if (currentTimeAttack < timeDamage)
        {
            if (currentIntervalTimeAttack >= intervalTimeDamage)
            {
                stat.healthPoint = stat.healthPoint - attackPoint;
                currentIntervalTimeAttack = 0;
                ChangeHealthBar();
            }
            
        }
        else
        {
            stateH = State.Idle;
            attackPoint = 0;
            timeDamage = 0;
            intervalTimeDamage = 0;
        }
        
    }

    private void Update()
    {
        switch (stateH)
        {
            default:
            case State.Idle:
                break;

            case State.ContinuosDamage:
                currentTimeAttack += Time.deltaTime;
                currentIntervalTimeAttack += Time.deltaTime;
                AttackTimeDamage();
                break;

        }

        if (isRecovery)
        {
            currentTimeRecovery += Time.deltaTime;
            currentIntervalTimeRecovery += Time.deltaTime;
            HealthTimeRecovery();
        }
    }
}
