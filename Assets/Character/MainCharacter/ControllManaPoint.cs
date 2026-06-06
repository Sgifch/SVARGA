using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllManaPoint : MonoBehaviour
{
    //public int currentMana;

    public PlayerStatManager playerStat;

    private float currentManaPoint;
    public GameObject manaBar;

    private Coroutine _manaRecovery;

    private void Start()
    {
        ChangeManaBar();
    }

    public void ChangeManaBar()
    {
        currentManaPoint = (float)(playerStat.currentMana) / playerStat.currentMaxMana;
        manaBar.GetComponent<Image>().fillAmount = currentManaPoint;
    }

    public void AddManaPoint(int point)
    {
        if (playerStat.currentMana + point < playerStat.currentMaxMana)
        {
            playerStat.currentMana += point;
        }
        else
        {
            playerStat.currentMana = playerStat.currentMaxMana;
        }

        ChangeManaBar();
    }

    public void SubstractManaPoint(int point)
    {
        if (playerStat.currentMana - point >= 0)
        {
            playerStat.currentMana -= point;
        }
        else
        {
            playerStat.currentMana = 0;
        }

        ChangeManaBar();
    }

    public void FullRecoveryMana()
    {
        playerStat.currentMana = playerStat.maxMana;
        ChangeManaBar();
    }

    public float Current()
    {
        return playerStat.currentMana;
    }
}
