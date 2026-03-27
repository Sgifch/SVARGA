using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPoisoned : MonoBehaviour
{
    public Item item;
    private int _damage;
    private int _strong;
    private int sumDamage;

    public int pDamage;
    public float chance;
    public float pTime;
    public float pInterval;

    private void Start()
    {
        itemScriptableObject _Item = gameObject.GetComponent<Item>().item;
        swordItem _sworditem = (swordItem)_Item;
        _damage = _sworditem.damage;
        _strong = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>().currentStrong;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyControllHealthPoint enemyProfile = collision.gameObject.GetComponent<EnemyControllHealthPoint>();
            if (CriticalDamage())
            {
                enemyProfile.DurationDamage(pDamage, pTime, pInterval);
            }
            else
            {
                sumDamage = _damage + _strong;
                GameObject.FindWithTag("UIControll").GetComponent<UIControll>().DamageUI(sumDamage);
                enemyProfile.Damage(sumDamage);
            }
                
        }
    }

    private bool CriticalDamage()
    {
        float n = Random.Range(0f, 1f);
        if (n <= chance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
