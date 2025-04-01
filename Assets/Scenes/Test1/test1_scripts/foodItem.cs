using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "foodItem", menuName ="Inventory/Items/New foodItem")] //создания ассета
public class foodItem : itemScriptableObject
{
    public float healthAmount;

    private void Start()
    {
        itemType = ItemType.food; //тип item
    }
}
