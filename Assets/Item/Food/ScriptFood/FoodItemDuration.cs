using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "foodItemDuration", menuName = "Inventory/Items/New foodItemDuration")]
public class FoodItemDuration : itemScriptableObject
{
    public int healthAmount;
    public float time;
    public float interval;

    private void Start()
    {
        itemType = ItemType.foodDuration;
    }
}
