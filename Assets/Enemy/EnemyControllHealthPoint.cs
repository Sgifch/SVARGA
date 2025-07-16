using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllHealthPoint : MonoBehaviour
{
    public EnemyScriptableObject enemyProfile;

    public float currentHealthPoint;
    public GameObject effectDeath;
    public Vector3 shiftSpawnEffect;
    private void Start()
    {
        currentHealthPoint = enemyProfile.healthPoint;
    }
    public void Damage(int _attackPoint)
    {
        currentHealthPoint -= _attackPoint;
        if(currentHealthPoint <= 0)
        {
            Dead();
        }  
    }

    public void Dead()
    {
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnEffect;
        Instantiate(effectDeath, spawnPosition, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
