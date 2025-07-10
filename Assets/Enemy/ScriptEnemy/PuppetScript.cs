using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetScript : MonoBehaviour
{
    public EnemyScriptableObject puppetStats;
    public int currentHealthPoint;
    public bool isDead = false;
    public GameObject effectDeath;
    public Vector3 shiftSpawnEffect;

    private void Start()
    {
        currentHealthPoint = puppetStats.healthPoint;
    }

    public void Dead()
    {
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnEffect;
        Instantiate(effectDeath, spawnPosition, gameObject.transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int _damage)
    {
        currentHealthPoint -= _damage;

        if (currentHealthPoint <= 0)
        {
            Dead();
        }
    }
}
