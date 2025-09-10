using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class CardsOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        itemScriptableObject item = gameObject.GetComponent<Item>().item;
        int amount = gameObject.GetComponent<Item>().amount;
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().AddItem(item, amount);
        //GameObject.FindWithTag("Player").GetComponent<UIControll>().FontainMenu(null); потом сделать когда буду переделывать ui
        for (int i=0; i<transform.parent.childCount; i++)
        {
            Destroy(transform.parent.GetChild(i).gameObject);
        }

    }

}
