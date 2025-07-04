using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Item item;
    public int _damage;

    private void Start()
    {
        itemScriptableObject _Item = gameObject.GetComponent<Item>().item;
        swordItem _sworditem = (swordItem)_Item;
        _damage = _sworditem.damage;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            PuppetScript enemyProfile = collision.gameObject.GetComponent<PuppetScript>(); // ��� ��� ���������� ��� ���� ������ � �� ����� ��� ������� ������ � ���������
            enemyProfile.TakeDamage(_damage);
        }
    }
}
