using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Cards/New Bonus")]
public class Bonus : ScriptableObject
{
    public string bonusName;
    public int bonusUnit;
}
