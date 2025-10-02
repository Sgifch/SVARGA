using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStatManager : MonoBehaviour
{
    //«десь будут данные игрока которые должны сохран€тьс€
    //—юда нужно будет перенести здоровье, манну спелы и тд

    public int maxHP;
    public int currentMaxHP; //если этот стат будет работать то надо потом подправить
    public int currentHP;

    public int maxManna;
    public int currentManna;

    public int strong;

    public int exp;
    public int lvl;
    public int upPoint;

    public int currentStrong;
    public int _currentHealthBonus;

    private void Start()
    {
        currentStrong = strong;
        currentMaxHP = maxHP;
        currentMaxHP += _currentHealthBonus;
    }

    public void SaveStat()
    {
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("lvl", lvl);
        PlayerPrefs.SetInt("upPoint", upPoint);

        PlayerPrefs.SetInt("maxHP", maxHP);
        PlayerPrefs.SetInt("maxManna", maxManna);
        PlayerPrefs.SetInt("strong", strong);
        PlayerPrefs.SetInt("currentHP", currentHP);

        GameObject panellArmor = GameObject.FindWithTag("UIControll").GetComponent<UIControll>().inventoryArmor;
        for (int i = 0; i<panellArmor.transform.childCount; i++)
        {
            _currentHealthBonus += panellArmor.transform.GetChild(i).GetComponent<EquipmentInventory>().currentHealthBonus;
        }

        PlayerPrefs.SetInt("_currentHealthBonus", _currentHealthBonus);
    }

    public void LoadStat()
    {
        lvl = PlayerPrefs.GetInt("lvl");
        exp = PlayerPrefs.GetInt("exp");
        upPoint = PlayerPrefs.GetInt("upPoint");

        maxHP = PlayerPrefs.GetInt("maxHP");
        maxManna = PlayerPrefs.GetInt("maxManna");
        strong = PlayerPrefs.GetInt("strong");
        currentHP = PlayerPrefs.GetInt("currentHP");
        _currentHealthBonus = PlayerPrefs.GetInt("_currentHealthBonus");
        //currentMaxHP = PlayerPrefs.GetInt("currentMaxHP");
    }
}
