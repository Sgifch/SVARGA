using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRastyKrovadScript : MonoBehaviour
{
    public Item item;
    private int _damage;
    private int _strong;
    private int sumDamage;

    public int addDamage;
    public int hpDamage;


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
            GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().Damage(hpDamage);
            EnemyControllHealthPoint enemyProfile = collision.gameObject.GetComponent<EnemyControllHealthPoint>();
            sumDamage = _damage + _strong + addDamage;
            GameObject.FindWithTag("UIControll").GetComponent<UIControll>().DamageUI(sumDamage);
            enemyProfile.Damage(sumDamage);
        }
    }
}
