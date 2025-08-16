using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    //«десь будут данные игрока которые должны сохран€тьс€
    //—юда нужно будет перенести здоровье, манну спелы и тд

    public int maxHP;
    public int currentHP;
    public int maxManna;
    public int currentManna;
    public int strong;

    public int exp;
    public int lvl;
    public int upPoint;

    public void SaveStat()
    {
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("lvl", lvl);
        PlayerPrefs.SetInt("upPoint", upPoint);

        PlayerPrefs.SetInt("maxHP", maxHP);
        PlayerPrefs.SetInt("maxManna", maxManna);
        PlayerPrefs.SetInt("strong", strong);
    }

    public void LoadStat()
    {
        lvl = PlayerPrefs.GetInt("lvl");
        exp = PlayerPrefs.GetInt("exp");
        upPoint = PlayerPrefs.GetInt("upPoint");

        maxHP = PlayerPrefs.GetInt("maxHP");
        maxManna = PlayerPrefs.GetInt("maxManna");
        strong = PlayerPrefs.GetInt("strong");
    }
}
