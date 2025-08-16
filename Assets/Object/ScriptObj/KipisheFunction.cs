using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KipisheFunction : MonoBehaviour
{
    public GameObject upgradeMenu;
    public PlayerStatManager playerStat;
    public characterStat playerConfig;

    public GameObject barHP;
    public GameObject barManna;
    public GameObject barStrong;
    public TMP_Text upPointText;

    public int maxUpgradeHP;
    private int maxUpgradeManna;
    private int maxUpgradeStrong;

    private void Start()
    {
        maxUpgradeHP = playerConfig.baseHP;
        maxUpgradeManna = playerConfig.baseManna;
        maxUpgradeStrong = playerConfig.baseStrong;
        CountingMaxStat();
        ShowKipishe();
    }
    public void UpgradeHealth()
    {
        if (playerStat.upPoint > 0)
        {
            if (playerStat.maxHP < maxUpgradeHP)
            {
                playerStat.maxHP += playerConfig.upgradeHP[playerStat.lvl];
                playerStat.upPoint--;
                ShowKipishe();
            }
        }
    }

    public void UpgradeMagic()
    {
        if (playerStat.upPoint > 0)
        {
            if (playerStat.maxManna < maxUpgradeManna)
            {
                playerStat.maxManna += playerConfig.upgradeManna[playerStat.lvl];
                playerStat.upPoint--;
                ShowKipishe();
            }
        }
    }

    public void UpgradeStrong()
    {
        if (playerStat.upPoint > 0)
        {
            if (playerStat.strong < maxUpgradeStrong)
            {
                playerStat.strong += playerConfig.upgradeStrong[playerStat.lvl];
                playerStat.upPoint--;
                ShowKipishe();
            }

        }
    }

    public void ShowKipishe()
    {
        float currentUpgradeHP = (float)playerStat.maxHP / maxUpgradeHP;
        barHP.GetComponent<Image>().fillAmount = currentUpgradeHP;

        float currentUpgradeManna = (float)playerStat.maxManna / maxUpgradeManna;
        barManna.GetComponent<Image>().fillAmount = currentUpgradeManna;

        float currentUpgradeStrong = (float)playerStat.strong / maxUpgradeStrong;
        barStrong.GetComponent<Image>().fillAmount = currentUpgradeStrong;

        upPointText.text = playerStat.upPoint.ToString();

        if (playerStat.maxHP == playerConfig.baseHP)
        {
            barHP.GetComponent<Image>().fillAmount = 0f;
        }

        if (playerStat.maxManna == playerConfig.baseManna)
        {
            barManna.GetComponent<Image>().fillAmount = 0f;
        }

        if (playerStat.strong == playerConfig.baseStrong)
        {
            barStrong.GetComponent<Image>().fillAmount = 0f;
        }
    }

    public void CountingMaxStat()
    {
        foreach (int stat in playerConfig.upgradeHP)
        {
            maxUpgradeHP += stat;
        }

        foreach (int stat in playerConfig.upgradeManna)
        {
            maxUpgradeManna += stat;
        }

        foreach (int stat in playerConfig.upgradeStrong)
        {
            maxUpgradeStrong += stat;
        }
    }

}
