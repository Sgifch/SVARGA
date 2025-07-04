using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { puppet }
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyType enemyType;
    public string name;
    public int healthPoint;
    public int attackPoint;
 
}
