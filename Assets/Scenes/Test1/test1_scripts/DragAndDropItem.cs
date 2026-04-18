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
        /*if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
            GameObject itemObject = Instantiate(oldSlot.item.itemObject, player.position + Vector3.up + player.forward, Quaternion.identity);
            // Устанавливаем количество объектов такое какое было в слоте
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            // убираем значения InventorySlot
            NullifySlotData();
        }*/
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<inventorySlot>() != null)
            {
                //Перемещаем данные из одного слота в другой
                ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<inventorySlot>());
            }
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
        // Защита от null
        if (newSlot == null) return;

        // 1. Сохраняем ВСЕ данные перед изменениями
        itemScriptableObject oldSlotItem = oldSlot.item;
        int oldSlotAmount = oldSlot.amount;
        bool oldSlotIsEmpty = oldSlot.isEmpty;

        itemScriptableObject newSlotItem = newSlot.item;
        int newSlotAmount = newSlot.amount;
        bool newSlotIsEmpty = newSlot.isEmpty;

        // 2. Проверки совместимости (только если слоты не пустые)
        if (!oldSlotIsEmpty)
        {
            if (newSlot.weaponSlot && oldSlotItem.itemType != ItemType.sword)
                return;
            if (newSlot.equipmentSlot && oldSlotItem.itemType != ItemType.amulet)
                return;
            if (newSlot.spellSlot && oldSlotItem.itemType != ItemType.magicBook)
                return;
        }

        if (!newSlotIsEmpty)
        {
            if (oldSlot.weaponSlot && newSlotItem.itemType != ItemType.sword)
                return;
            if (oldSlot.equipmentSlot && newSlotItem.itemType != ItemType.amulet)
                return;
            if (oldSlot.spellSlot && newSlotItem.itemType != ItemType.magicBook)
                return;
        }

        // 3. Снимаем бонусы с обоих слотов (если они экипированы и не пустые)
        if (oldSlot.equipmentSlot && !oldSlotIsEmpty)
        {
            oldSlot.GetComponent<EquipmentInventory>().UnequipmentAmulet();
        }

        if (newSlot.equipmentSlot && !newSlotIsEmpty)
        {
            newSlot.GetComponent<EquipmentInventory>().UnequipmentAmulet();
        }

        // 4. Обмениваем данные
        oldSlot.item = newSlotItem;
        oldSlot.amount = newSlotAmount;
        oldSlot.isEmpty = newSlotIsEmpty;

        newSlot.item = oldSlotItem;
        newSlot.amount = oldSlotAmount;
        newSlot.isEmpty = oldSlotIsEmpty;

        // 5. Обновляем UI для обоих слотов
        UpdateSlotUI(oldSlot, newSlotItem, newSlotAmount, newSlotIsEmpty);
        UpdateSlotUI(newSlot, oldSlotItem, oldSlotAmount, oldSlotIsEmpty);

        // 6. Одеваем бонусы на слоты (если они экипированы и не пустые)
        if (oldSlot.equipmentSlot && !oldSlot.isEmpty)
        {
            oldSlot.GetComponent<EquipmentInventory>().EquipmentAmulet();
        }

        if (newSlot.equipmentSlot && !newSlot.isEmpty)
        {
            newSlot.GetComponent<EquipmentInventory>().EquipmentAmulet();
        }
    }

    private void UpdateSlotUI(inventorySlot slot, itemScriptableObject item, int amount, bool isEmpty)
    {
        if (slot == null) return;

        if (isEmpty || item == null)
        {
            if (slot.iconItem != null)
            {
                slot.iconItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slot.iconItem.GetComponent<Image>().sprite = null;
            }
            if (slot.itemAmount != null)
            {
                slot.itemAmount.text = "";
            }
        }
        else
        {
            if (slot.iconItem != null)
            {
                slot.SetIcon(item.icon);
            }
            if (slot.itemAmount != null)
            {
                slot.itemAmount.text = amount.ToString();
            }
        }
    }
}
