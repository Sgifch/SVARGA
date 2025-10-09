using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public inventorySlot slot;
    public int currentHealthBonus = 0;
    public int currentStrongBonus = 0;
    public PlayerStatManager playerStatManager;

    public void Start()
    {
        //slots = gameObject.GetComponent<inventoryManager>().slotsWeapon;
        playerStatManager = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();
        slot = gameObject.GetComponent<inventorySlot>();
        playerStatManager.currentMaxHP += currentHealthBonus;
        playerStatManager.currentStrong += currentStrongBonus;

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
                case "strong":
                    currentStrongBonus += _bonus.bonusUnit;
                    break;
            }
        }

        playerStatManager.currentMaxHP += currentHealthBonus;
        playerStatManager.currentStrong += currentStrongBonus;
        GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().ChangeHealthBar();

    }
    public void UnequipmentAmulet()
    {
        playerStatManager.currentMaxHP -= currentHealthBonus;
        if (playerStatManager.currentHP > playerStatManager.currentMaxHP)
        {
            playerStatManager.currentHP = playerStatManager.currentMaxHP;
        }
        currentHealthBonus = 0;

        playerStatManager.currentStrong -= currentStrongBonus;
        GameObject.FindWithTag("Player").GetComponent<ControllHealthPoint>().ChangeHealthBar();
    }
}
