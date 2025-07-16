using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefalautEnemy", menuName = "Enemy/New Defalaut Enemy")]
public class DefalautEnemy : EnemyScriptableObject
{
    private void Start()
    {
        enemyType = EnemyType.defalaut;
    }
}
