using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public inventorySlot oldSlot;
    private Transform player;

    private void Start()
    {
        //ПОСТАВЬТЕ ТЭГ "PLAYER" НА ОБЪЕКТЕ ПЕРСОНАЖА!
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Находим скрипт InventorySlot в слоте в иерархии
        oldSlot = transform.GetComponentInParent<inventorySlot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Если слот пустой, то мы не выполняем то что ниже return;
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        //Делаем картинку прозрачнее
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
        transform.SetParent(transform.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
            GameObject itemObject = Instantiate(oldSlot.item.itemObject, player.position + Vector3.up + player.forward, Quaternion.identity);
            // Устанавливаем количество объектов такое какое было в слоте
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            // убираем значения InventorySlot
            NullifySlotData();
        }
        else if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<inventorySlot>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<inventorySlot>());
        }
       
    }
    void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconItem.GetComponent<Image>().sprite = null;
        oldSlot.itemAmount.text = "";
    }
    void ExchangeSlotData(inventorySlot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        itemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconItem;
        TMP_Text itemAmountText = newSlot.itemAmount;

        if (newSlot.weaponSlot)
        {
            if (!(oldSlot.item.itemType is ItemType.sword))
            {
                return;
                /*newSlot.item = oldSlot.item;
                newSlot.amount = oldSlot.amount;*/
            }
        }
        if (newSlot.equipmentSlot)
        {
            if(!(oldSlot.item.itemType is ItemType.amulet))
            {
                return;
            }
            /*else
            {
                print("Это амулет");
                newSlot.GetComponent<EquipmentInventory>().EquipmentAmulet();
            }*/
        }

        // Заменяем значения newSlot на значения oldSlot
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;

        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(newSlot.item.icon);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconItem.GetComponent<Image>().sprite = null;
            newSlot.itemAmount.text = "";
        }
        
        if (newSlot.equipmentSlot)
        {
            newSlot.GetComponent<EquipmentInventory>().EquipmentAmulet();
        }
      
        newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(oldSlot.item.icon);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.iconItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconItem.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }

        oldSlot.isEmpty = isEmpty;

        if (oldSlot.equipmentSlot)
        {
            oldSlot.GetComponent<EquipmentInventory>().UnequipmentAmulet();
        }
    }
}
