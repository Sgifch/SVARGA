using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsPoint", menuName = "Player/StatsPoint")]
public class characterStat : ScriptableObject
{
    //Описание статов, которые будут улучшены
    //Здесь должны быть просто настройки и базовые значения не более
    public int maxHealthPoint;
    public int maxMannaPoint;
    public int healthPoint;
    public int mannaPoint;

    public int baseHP;
    public int baseManna;
    public int baseStrong;

    public List<int> upgradeHP = new List<int>();
    public List<int> upgradeManna = new List<int>();
    public List<int> upgradeStrong = new List<int>();

    public List<int> maxExp = new List<int>();

}



