using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puppet", menuName = "Enemy/newPuppet")]
public class Puppet : EnemyScriptableObject
{
    private void Start()
    {
        enemyType = EnemyType.puppet;
    }

}
