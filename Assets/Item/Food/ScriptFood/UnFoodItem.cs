using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unFoodItem", menuName = "Inventory/Items/New unFoodItem")]
public class UnFoodItem : itemScriptableObject
{
    public int unHealthAmount;

    private void Start()
    {
        itemType = ItemType.unFood;
    }
}
