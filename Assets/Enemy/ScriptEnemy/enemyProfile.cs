using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStaticProfile", menuName = "Enemy/EnemyStatic")]
public class enemyProfile : ScriptableObject
{
    public string name;
    public int attack;
}
