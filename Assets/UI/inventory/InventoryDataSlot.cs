using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class InventoryDataSlot
{
    public itemScriptableObject item;
    public int amount;
    public bool isEmpty;
    public bool weaponSlot;
    public bool equipmentSlot;

    public InventoryDataSlot () { }

    public InventoryDataSlot(inventorySlot slot)
    {

        amount = slot.amount;
        isEmpty = slot.isEmpty;
        weaponSlot = slot.weaponSlot;
        equipmentSlot = slot.equipmentSlot;

        if (slot.item != null)
        {
            item = slot.item;
        }
        else
        {
            item = null;
        }
    }
}
