using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBetterSteelScript : MonoBehaviour
{
    public Item item;
    private int _damage;
    private int _strong;
    private int sumDamage;

    public float chance;
    public int critDamage;

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
            sumDamage = _damage + _strong + CriticalDamage();
            GameObject.FindWithTag("UIControll").GetComponent<UIControll>().DamageUI(sumDamage);
            enemyProfile.Damage(sumDamage);
        }
    }

    private int CriticalDamage()
    {
        float n = Random.Range(0f, 1f);
        if (n <= chance)
        {
            return critDamage;
        }
        else
        {
            return 0;
        }
    }
}
