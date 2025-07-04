using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetScript : MonoBehaviour
{
    public EnemyScriptableObject puppetStats;
    public int currentHealthPoint;
    public bool isDead = false;
    private void Start()
    {
        currentHealthPoint = puppetStats.healthPoint;
    }

    public void Dead()
    {
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
