using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardItem", menuName = "Cards/New Amulet")]
public class CradsItem : itemScriptableObject
{
    private void Start()
    {
        itemType = ItemType.amulet;
    }
}
