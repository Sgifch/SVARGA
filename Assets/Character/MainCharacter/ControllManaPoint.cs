using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllManaPoint : MonoBehaviour
{
    public PlayerStatManager playerStat;
    public float flashTime = 1f;
    public AnimationCurve flashCurve;
    public GameObject effectDamage;
    public GameObject effectRecovery;

    public bool useGameManager;

    private GameObject gameManager;
    private float currentManaPoint; //Текущие очки
    private GameObject manaBar;
    private Material material;

    private Coroutine _manaRecovery;

    public void ChangeManaBar()
    {
        currentManaPoint = (float)(playerStat.currentHP) / playerStat.currentMaxHP;
        manaBar.GetComponent<Image>().fillAmount = currentManaPoint;
    }
}
