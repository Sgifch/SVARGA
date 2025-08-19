using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Item item;
    public int _damage;
    public int _strong;
    private int sumDamage;

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
            sumDamage = _damage + _strong;

            enemyProfile.Damage(sumDamage);
        }
    }
}
