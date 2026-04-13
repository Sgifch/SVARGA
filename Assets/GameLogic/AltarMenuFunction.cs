using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AltarMenuFunction : MonoBehaviour
{
    public int maxSouls;
    //Сохранить
    //public int currentSouls;
    public PlayerStatManager stat;
    public GameObject countSouls;
    void Start()
    {
        UpdateAltar();
    }

    public void UpdateAltar()
    {
        float souls = (float)(stat.currentSouls) / maxSouls;
        countSouls.GetComponent<Image>().fillAmount = souls;
    }

    public void GetSouls()
    {
        inventoryManager inventory = GameObject.FindWithTag("Player").GetComponent<inventoryManager>();
        List<inventorySlot> slots = inventory.slots;

        foreach (inventorySlot _slots in slots)
        {
            if (_slots.item != null)
            {
                if (_slots.item.itemType == ItemType.soul)
                {
                    stat.currentSouls += _slots.amount;
                    inventory.DestroyItem(_slots);
                }
            }
        }

        UpdateAltar();
        
        if (maxSouls >= stat.currentSouls)
        {
            CompleteAltar();
        }
    }

    public void CompleteAltar()
    {

    }
}
