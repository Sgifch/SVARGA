using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllStat : MonoBehaviour
{
    public characterStat playerConfig;
    public PlayerStatManager playerStat;
    
    public Image expBar;
    public TMP_Text expText;
    public TMP_Text levelText;
    void Awake()
    {
        playerStat = GameObject.Find("PlayerStatManager").GetComponent<PlayerStatManager>();

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

        if (playerStat.exp >= playerConfig.maxExp[playerStat.lvl])
        {
            playerStat.exp -= playerConfig.maxExp[playerStat.lvl];
            playerStat.lvl++;
            playerStat.upPoint++;
        }

        ShowExpInformation();
    }

    public void ShowExpInformation()
    {
        levelText.text = (playerStat.lvl + 1).ToString();
        expText.text = playerStat.exp.ToString() + " / " + playerConfig.maxExp[playerStat.lvl].ToString();

        float currentExp = (float)(playerStat.exp) / playerConfig.maxExp[playerStat.lvl];
        expBar.fillAmount = currentExp;
    }
}
