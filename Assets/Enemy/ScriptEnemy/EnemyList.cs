using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Enemy/Enemy list")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> enemyList = new List<GameObject>();
}
