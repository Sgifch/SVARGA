using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using static UIControll;

public class CardsOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        itemScriptableObject item = gameObject.GetComponent<Item>().item;
        int amount = gameObject.GetComponent<Item>().amount;
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().AddItem(item, amount);
        for (int i=0; i<transform.parent.childCount; i++)
        {
            Destroy(transform.parent.GetChild(i).gameObject);
        }

        CardsDestroy();

    }

    public void CardsDestroy()
    {
        GameObject uiControll= GameObject.FindWithTag("UIControll");
        uiControll.GetComponent<UIControll>().stateUI = StateUI.idle;
        uiControll.GetComponent<UIControll>().fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
        Destroy(uiControll.GetComponent<UIControll>().collision.transform.GetChild(0).gameObject);
        uiControll.GetComponent<UIControll>().isStay = false;
        uiControll.GetComponent<UIControll>().collision.GetComponent<FontainFunction>().isTake = true;
    }

}
