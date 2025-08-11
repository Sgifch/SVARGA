using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllStat : MonoBehaviour
{
    public characterStat playerStat;

    public Image expBar;
    public TMP_Text expText;
    public TMP_Text levelText;
    void Awake()
    {
        GameObject UI = GameObject.FindWithTag("UI");
        expBar = UI.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();
        expText = UI.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        levelText = UI.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
    }
    private void Start()
    {
        ShowExpInformation();
    }
    public void AddExp(int _exp)
    {
        playerStat.exp += _exp;

        if (playerStat.exp >= playerStat.maxExp[playerStat.lvl - 1])
        {
            playerStat.exp -= playerStat.maxExp[playerStat.lvl - 1];
            playerStat.lvl++;
            playerStat.upPoints++;
        }

        ShowExpInformation();
    }

    public void ShowExpInformation()
    {
        levelText.text = playerStat.lvl.ToString();
        expText.text = playerStat.exp.ToString() + " / " + playerStat.maxExp[playerStat.lvl - 1].ToString();

        float currentExp = (float)(playerStat.exp) / playerStat.maxExp[playerStat.lvl-1];
        expBar.fillAmount = currentExp;
    }
}
