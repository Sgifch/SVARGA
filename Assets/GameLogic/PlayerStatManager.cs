using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStatManager : MonoBehaviour
{
    //����� ����� ������ ������ ������� ������ �����������
    //���� ����� ����� ��������� ��������, ����� ����� � ��

    public int maxHP;
    public int currentMaxHP; //���� ���� ���� ����� �������� �� ���� ����� ����������
    public int currentHP;

    public int maxManna;
    public int currentManna;

    public int strong;

    public int exp;
    public int lvl;
    public int upPoint;

    public int currentStrong;

    private void Start()
    {
        currentStrong = strong;
        currentMaxHP = maxHP;

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
    }
}
