using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "foodItem", menuName ="Inventory/Items/New foodItem")] //�������� ������
public class foodItem : itemScriptableObject
{
    public int healthAmount;

    private void Start()
    {
        itemType = ItemType.food; //��� item
    }
}
