using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllManaPoint : MonoBehaviour
{
    public int currentMana;

    public PlayerStatManager playerStat;

    private float currentManaPoint;
    public GameObject manaBar;

    private Coroutine _manaRecovery;

    private void Start()
    {
        currentMana = playerStat.currentMaxMana;
        ChangeManaBar();
    }

    public void ChangeManaBar()
    {
        currentManaPoint = (float)(currentMana) / playerStat.currentMaxMana;
        manaBar.GetComponent<Image>().fillAmount = currentManaPoint;
    }

    public void AddManaPoint(int point)
    {
        if (currentMana + point < playerStat.currentMaxMana)
        {
            currentMana += point;
        }
        else
        {
            currentMana = playerStat.currentMaxMana;
        }

        ChangeManaBar();
    }

    public void SubstractManaPoint(int point)
    {
        if (currentMana - point >= 0)
        {
            currentMana -= point;
        }
        else
        {
            currentMana = 0;
        }

        ChangeManaBar();
    }
}
