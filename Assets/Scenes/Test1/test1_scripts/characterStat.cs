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
}
