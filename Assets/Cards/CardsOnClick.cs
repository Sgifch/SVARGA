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
        //GameObject.FindWithTag("Player").GetComponent<UIControll>().FontainMenu(null); потом сделать когда буду переделывать ui
        for (int i=0; i<transform.parent.childCount; i++)
        {
            Destroy(transform.parent.GetChild(i).gameObject);
        }

        CardsDestroy();

    }

    public void CardsDestroy()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<UIControll>().stateUI = StateUI.idle;
        player.GetComponent<UIControll>().fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
        Destroy(player.GetComponent<UIControll>().fontain.transform.parent.GetChild(0).gameObject);
        //fontainMenu.SetActive(false);
        player.GetComponent<UIControll>().fontain = null;
        player.GetComponent<UIControll>().isFontain = false;
        player.GetComponent<inventoryManager>().isOpened = false;
    }

}
