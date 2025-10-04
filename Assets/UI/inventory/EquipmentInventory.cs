using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public inventorySlot slot;
    public int currentHealthBonus = 0;
    public GameObject playerStatManager;

    public void Start()
    {
        //slots = gameObject.GetComponent<inventoryManager>().slotsWeapon;
        playerStatManager = GameObject.FindWithTag("PlayerStatManager");
        slot = gameObject.GetComponent<inventorySlot>();
        playerStatManager.GetComponent<PlayerStatManager>().currentMaxHP += currentHealthBonus;

    }
    public void EquipmentAmulet()
    {
        CradsItem amulet = (CradsItem)slot.item;
        foreach (Bonus _bonus in amulet.bonusList)
        {
            switch (_bonus.bonusName)
            {
                case "health":
                    currentHealthBonus += _bonus.bonusUnit;
                    break;
            }
        }

        playerStatManager.GetComponent<PlayerStatManager>().currentMaxHP += currentHealthBonus;
        GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().ChangeHealthBar();

    }
    public void UnequipmentAmulet()
    {
        playerStatManager.GetComponent<PlayerStatManager>().currentMaxHP -= currentHealthBonus;
        currentHealthBonus = 0;
        GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().ChangeHealthBar();
    }
}
