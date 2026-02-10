using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public inventorySlot slot;

    //Текущие-бонусы
    public int currentHealthBonus = 0;
    public int currentStrongBonus = 0;
    public int currentManaBonus = 0;

    public PlayerStatManager playerStatManager;
    private GameObject player;
    private UIControll uiControll;

    public void Start()
    {
        //slots = gameObject.GetComponent<inventoryManager>().slotsWeapon;
        player = GameObject.FindWithTag("Player");
        uiControll = GameObject.FindWithTag("UIControll").GetComponent<UIControll>();
        playerStatManager = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();

        slot = gameObject.GetComponent<inventorySlot>();
        playerStatManager.currentMaxHP += currentHealthBonus;
        playerStatManager.currentStrong += currentStrongBonus;

    }

    //Прибавление-бонусов
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
                case "mana":
                    currentManaBonus += _bonus.bonusUnit;
                    break;
            }
        }

        playerStatManager.currentMaxHP += currentHealthBonus;
        playerStatManager.currentStrong += currentStrongBonus;
        playerStatManager.currentMaxMana += currentManaBonus;
        
        player.GetComponent<ControllHealthPoint>().ChangeHealthBar();
        uiControll.UpgradeInventory();

    }

    //Снятие-бонусов
    public void UnequipmentAmulet()
    {
        //Снятие-бонусов-здоровья
        playerStatManager.currentMaxHP -= currentHealthBonus;
        if (playerStatManager.currentHP > playerStatManager.currentMaxHP)
        {
            playerStatManager.currentHP = playerStatManager.currentMaxHP;
        }
        currentHealthBonus = 0;

        //Снятие-бонусов-силы
        playerStatManager.currentStrong -= currentStrongBonus;
        currentStrongBonus = 0;

        //Снятие-бонусов-маны
        playerStatManager.currentMaxMana -= currentManaBonus;
        if (playerStatManager.currentMana > playerStatManager.currentMaxMana)
        {
            playerStatManager.currentMana = playerStatManager.currentMaxMana;
        }
        currentManaBonus = 0;

        player.GetComponent<ControllHealthPoint>().ChangeHealthBar();
        uiControll.UpgradeInventory();

    }
}
