using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsPoint", menuName = "Player/StatsPoint")]
public class characterStat : ScriptableObject
{
    public int maxHealthPoint;
    public int maxMannaPoint;
    public int healthPoint;
    public int mannaPoint;

    public int exp;
    public int lvl = 1;
    public List<int> maxExp = new List<int>();
    public int upPoints; //«а эти очки можно будет что-то прокачать
}


