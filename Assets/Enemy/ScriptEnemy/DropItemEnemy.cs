using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemEnemy : MonoBehaviour
{
    public GameObject dropItem;
    public int min;
    public int max;
    
    public int DropRandom()
    {
        int n = Random.Range(min, max);
        return n;
    }

    public void DropInInventory()
    {
        int n = DropRandom();
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<inventoryManager>().AddItem(dropItem.GetComponent<Item>().item, n);
        print("+soul");
    }
}
