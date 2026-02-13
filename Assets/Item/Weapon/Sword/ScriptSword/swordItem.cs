using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "swordItem", menuName = "Inventory/Items/New swordItem")] //создания ассета
public class swordItem : itemScriptableObject
{
    public int damage;
    public GameObject effect;
    private void Start()
    {
        itemType = ItemType.sword; //тип item
    }

}
