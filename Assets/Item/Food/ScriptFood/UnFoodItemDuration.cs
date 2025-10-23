using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unFoodItemDuration", menuName = "Inventory/Items/New unFoodItemDuration")]
public class UnFoodItemDuration : itemScriptableObject
{
    public int unHealthAmount;
    public float time;
    public float interval;

    private void Start()
    {
        itemType = ItemType.unFoodDuration;
    }
}
