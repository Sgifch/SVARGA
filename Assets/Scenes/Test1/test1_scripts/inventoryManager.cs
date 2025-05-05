using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform inventoryPanel;

    public GameObject UIPanelFast;
    public Transform inventoryPanelFast;

    public GameObject UIPanelArmor;
    public Transform inventoryArmor;

    public GameObject UIPanelWeapon;
    public Transform inventoryWeapon;

    public GameObject UIPanelWeaponFast;
    public Transform inventoryWeaponFast;

    public GameObject barPanel;

    public bool isOpened;

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //Список инвентаря
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //Список быстрого инвентаря
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //возможно стоит переделать слотс ну это в будущем
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();

    void Start()
    {
        UIPanel.SetActive(false);

        for (int i=0; i < inventoryPanel.childCount; i++) //мб этот блок потом засунуть в одну функцию для удобства
        {
            if (inventoryPanel.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i=0; i < inventoryPanelFast.childCount; i++)
        {
            if (inventoryPanelFast.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsFast.Add(inventoryPanelFast.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryArmor.childCount; i++)
        {
            if (inventoryArmor.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsArmor.Add(inventoryArmor.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryWeapon.childCount; i++)
        {
            if (inventoryWeapon.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsWeapon.Add(inventoryWeapon.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryWeaponFast.childCount; i++)
        {
            if (inventoryWeaponFast.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsWeaponFast.Add(inventoryWeaponFast.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Открытие меню и закрытие предметов быстрого доступа
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanelFast.SetActive(false);
                UIPanel.SetActive(true);
                barPanel.SetActive(false);
            }
            else
            {
                UIPanelFast.SetActive(true);
                UIPanel.SetActive(false);
                barPanel.SetActive(true);
            }
        }

        if (collisionStay) //Подбирается предмет
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collision.gameObject.tag == "item")
                {
                    AddItem(collision.gameObject.GetComponent<Item>().item, collision.gameObject.GetComponent<Item>().amount);
                    Destroy(collision.gameObject);
                }
            }
        }

        CopySlots(); //Копирование предметов из первых 6 слотов
                     //Игрок не должен с ними взаимодействовать
                     //Они нужны только для отображения предметов

    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionStay = true;
        this.collision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionStay = false;
        this.collision = collision;
    }

    private void AddItem(itemScriptableObject _item, int _amount)
    {
        foreach (inventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount  <= _item.maximumAmaunt)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return;
                }
                break;
            }
        }
        foreach (inventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                break;
            }
        }
       
    }

    private void CopySlots() //Копирование предметов из первых 6 слотов
    {
        for (int i =0; i < inventoryPanelFast.childCount; i++) //Скорее всего тут есть проблема с тем когда слот остаётся пкстым
        {
            if (slots[i].isEmpty == false)
            {
                slotsFast[i].itemAmount.text = slots[i].amount.ToString();
                slotsFast[i].SetIcon(slots[i].item.icon);
            }
            else
            {
                slotsFast[i].itemAmount.text = "";
                slotsFast[i].SetIcon(null);
            }
        }

        //Для части слотов с отображением оружия нужно потом переделать
        for (int i = 0; i < inventoryWeaponFast.childCount; i++)
        {
            if (slotsWeapon[i].isEmpty == false)
            {
                slotsWeaponFast[i].itemAmount.text = slotsWeapon[i].amount.ToString();
                slotsWeaponFast[i].SetIcon(slots[i].item.icon);
            }
        }
    }

    //Выбор предмета в одном из 6 слотов
    private void SelectFastSlot()
    {

    }

}
