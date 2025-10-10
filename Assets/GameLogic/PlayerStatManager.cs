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
    public int currentMaxManna;
    public int currentManna;

    public int strong;

    public int exp;
    public int lvl;
    public int upPoint;

    public int currentStrong;
    public int _currentHealthBonus;
    public List<int> currentHealthBonus = new List<int>(); 

    private void Start()
    {
        currentStrong = strong;
        currentMaxHP = maxHP;
        currentMaxManna = maxManna;
        currentMaxHP += _currentHealthBonus;
        GameObject.FindWithTag("UIControll").GetComponent<UIControll>().UpgradeInventory();
        //¬озможно здесь прописать обновление бара
    }

    public void SaveStat()
    {
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("lvl", lvl);
        PlayerPrefs.SetInt("upPoint", upPoint);

        PlayerPrefs.SetInt("maxHP", maxHP);
        PlayerPrefs.SetInt("maxManna", maxManna);
        PlayerPrefs.SetInt("currentMaxManna", currentMaxManna);
        PlayerPrefs.SetInt("strong", strong);
        PlayerPrefs.SetInt("currentHP", currentHP);

        GameObject panellArmor = GameObject.FindWithTag("UIControll").GetComponent<UIControll>().inventoryArmor;
        for (int i = 0; i<panellArmor.transform.childCount; i++)
        {
            _currentHealthBonus += panellArmor.transform.GetChild(i).GetComponent<EquipmentInventory>().currentHealthBonus;
            currentHealthBonus.Add(panellArmor.transform.GetChild(i).GetComponent<EquipmentInventory>().currentHealthBonus);
        }

        PlayerPrefs.SetInt("currentHealthBonus1", currentHealthBonus[0]);
        PlayerPrefs.SetInt("currentHealthBonus2", currentHealthBonus[1]);
        PlayerPrefs.SetInt("currentHealthBonus3", currentHealthBonus[2]);
        PlayerPrefs.SetInt("currentHealthBonus4", currentHealthBonus[3]);

        PlayerPrefs.SetInt("_currentHealthBonus", _currentHealthBonus);
    }

    public void LoadStat()
    {
        lvl = PlayerPrefs.GetInt("lvl");
        exp = PlayerPrefs.GetInt("exp");
        upPoint = PlayerPrefs.GetInt("upPoint");

        maxHP = PlayerPrefs.GetInt("maxHP");
        maxManna = PlayerPrefs.GetInt("maxManna");
        currentMaxManna = PlayerPrefs.GetInt("currentMaxManna");
        strong = PlayerPrefs.GetInt("strong");
        currentHP = PlayerPrefs.GetInt("currentHP");

        GameObject panellArmor = GameObject.FindWithTag("UIControll").GetComponent<UIControll>().inventoryArmor;
        panellArmor.transform.GetChild(0).GetComponent<EquipmentInventory>().currentHealthBonus = PlayerPrefs.GetInt("currentHealthBonus1");
        panellArmor.transform.GetChild(1).GetComponent<EquipmentInventory>().currentHealthBonus = PlayerPrefs.GetInt("currentHealthBonus2");
        panellArmor.transform.GetChild(2).GetComponent<EquipmentInventory>().currentHealthBonus = PlayerPrefs.GetInt("currentHealthBonus3");
        panellArmor.transform.GetChild(3).GetComponent<EquipmentInventory>().currentHealthBonus = PlayerPrefs.GetInt("currentHealthBonus4");
        _currentHealthBonus = PlayerPrefs.GetInt("_currentHealthBonus");
    }
}
