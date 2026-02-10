using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemEnemy : MonoBehaviour
{
    /* ВЫПАДЕНИЕ БОНУСОВ С ВРАГОВ */
    //Мб перенести сюда экспу

    //Выпадение-маны
    public int minM;
    public int maxM;

    //Выпадение-предметов
    public GameObject dropItem;
    public int min;
    public int max;
    
    public int DropRandom(int min, int max)
    {
        int n = Random.Range(min, max);
        return n;
    }

    public void DropInInventory()
    {
        int n = DropRandom(min, max);
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<inventoryManager>().AddItem(dropItem.GetComponent<Item>().item, n);
    }

    public void DropMana()
    {
        int n = DropRandom(minM, maxM);
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<ControllManaPoint>().AddManaPoint(n);
    }
}
