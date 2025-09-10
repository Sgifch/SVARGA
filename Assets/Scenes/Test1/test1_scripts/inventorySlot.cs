using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventorySlot : MonoBehaviour
{
    public itemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public bool weaponSlot = false;
    public bool equipmentSlot = false;
    public GameObject iconItem;
    public TMP_Text itemAmount;

    private void Awake()
    {
        iconItem = transform.GetChild(0).GetChild(0).gameObject;
        itemAmount = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        if (icon != null)
        {
            iconItem.GetComponent<Image>().color=new Color (1,1,1,1);
            iconItem.GetComponent<Image>().sprite = icon;
        }
        else
        {
            iconItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        
    }

    public void AddAmount(int amount)
    {
        itemAmount.text = amount.ToString();
    }
}
