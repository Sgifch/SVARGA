using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBloodSaberScript : MonoBehaviour
{
    public Item item;
    private int _damage;
    private int _strong;
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
            GameObject.FindWithTag("UIControll").GetComponent<UIControll>().DamageUI(sumDamage);
            enemyProfile.Damage(sumDamage);
            SwordDestroy();
        }
    }

    private void SwordDestroy()
    {
        inventoryManager inventory = GameObject.FindWithTag("Player").GetComponent<inventoryManager>();
        inventory.SubtractionItem(0, inventory.slotsWeapon);
    }
}
