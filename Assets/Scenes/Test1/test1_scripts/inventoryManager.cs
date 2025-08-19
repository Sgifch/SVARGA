using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    private GameObject UI;

    private GameObject inventory;
    private Transform inventoryPanel;

    private GameObject UIPanelFast;
    private Transform inventoryPanelFast;

    private GameObject UIPanelArmor;
    private Transform inventoryArmor;

    private GameObject UIPanelWeapon;
    private Transform inventoryWeapon;

    private GameObject UIWeaponFast;
    private Transform weaponFastPanel;

    private GameObject barPanel;
    public GameObject upgradeMenu;

    public bool isOpened;
    private bool isKipisheOpen = false;
    public int indexSlot; 

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //Список инвентаря
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //Список быстрого инвентаря
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //возможно стоит переделать слотс ну это в будущем
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    private void Awake()
    {
        UI = GameObject.Find("UI");

        inventory = UI.transform.GetChild(0).gameObject;
        inventoryPanel = inventory.transform;

        UIPanelArmor = inventoryPanel.GetChild(0).GetChild(1).GetChild(0).gameObject;
        inventoryArmor = UIPanelArmor.transform;

        UIPanelWeapon = inventoryPanel.GetChild(0).GetChild(1).GetChild(1).gameObject;
        inventoryWeapon = UIPanelWeapon.transform;

        UIPanelFast = UI.transform.GetChild(1).gameObject;
        inventoryPanelFast = UIPanelFast.transform;

        UIWeaponFast = UI.transform.GetChild(2).gameObject;
        weaponFastPanel = UIWeaponFast.transform;

        barPanel = UI.transform.GetChild(3).gameObject;

    }
    void Start()
    {
      

        for (int i=0; i < inventoryPanel.GetChild(0).GetChild(0).childCount; i++) //мб этот блок потом засунуть в одну функцию для удобства
        {
            if (inventoryPanel.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slots.Add(inventoryPanel.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i=0; i < inventoryPanelFast.GetChild(1).childCount; i++)
        {
            if (inventoryPanelFast.GetChild(1).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsFast.Add(inventoryPanelFast.GetChild(1).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
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

        //Слоты для отображения оружия
        for (int i = 0; i < weaponFastPanel.GetChild(0).childCount; i++)
        {
            if (weaponFastPanel.GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsWeaponFast.Add(weaponFastPanel.GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i=0; i < inventoryPanelFast.GetChild(1).childCount; i++)
        {
            selectSlot.Add(inventoryPanelFast.GetChild(1).GetChild(i).GetChild(1).gameObject);
        }

        //Картинки, указывающие на выбранный слот
        for (int i = 0; i < 6; i++)
        {

            selectSlot[i].SetActive(false);
        }

        selectSlot[0].SetActive(true);
        SelectSlot(1);

        inventory.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Открытие меню и закрытие предметов быстрого доступа
        {
            if (!gameObject.GetComponent<UIControll>().isOpen)
            {
                MenuControll();
            }
        }

        if (collisionStay) //Подбирается предмет
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collision.gameObject.tag == "item") //переделать под switchCase
                {
                    AddItem(collision.gameObject.GetComponent<Item>().item, collision.gameObject.GetComponent<Item>().amount);
                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.tag == "WeaponSpawn")
                {
                    if (!collision.gameObject.GetComponent<WeaponSpawnScript>().isEmpty)
                    {
                        GameObject _weapon = collision.gameObject.GetComponent<WeaponSpawnScript>().spawnItem;
                        AddItem(_weapon.GetComponent<Item>().item, 1);
                        collision.gameObject.GetComponent<WeaponSpawnScript>().TakeItem();
                    }
                }
                else if (collision.gameObject.tag == "Kipishe")
                {
                    gameObject.GetComponent<UIControll>().UphradeMenu();
                }

            }
        }

        //Выбор одного из 6 слотов
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(6);
        }

        if (Input.GetMouseButtonDown(1))
        {
            UseItem(indexSlot);
        }

        CopySlots(); //Копирование предметов из первых 6 слотов
                     //Игрок не должен с ними взаимодействовать
                     //Они нужны только для отображения предметов

    }

    public void ControllHUD(bool open)
    {
        if (open)
        {

        }
        else
        {

        }
    }
    public void MenuControll()
    {
        isOpened = !isOpened;
        if (isOpened)
        {
            UIPanelFast.SetActive(false);
            UIWeaponFast.SetActive(false);
            inventory.SetActive(true);
            barPanel.SetActive(false);
        }
        else
        {
            UIPanelFast.SetActive(true);
            UIWeaponFast.SetActive(true);
            inventory.SetActive(false);
            barPanel.SetActive(true);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionStay = true;
        this.collision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionStay = false;
        if (collision.gameObject.tag == "Kipishe")
        {
            gameObject.GetComponent<UIControll>().UpgradeMenuClose();
        }
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

    //Копирование слотов
    private void CopySlots()
    {
        for (int i =0; i < inventoryPanelFast.GetChild(1).childCount; i++)
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
        for (int i = 0; i < weaponFastPanel.GetChild(0).childCount; i++)
        {
            if (slotsWeapon[i].isEmpty == false)
            {
                //slotsWeaponFast[i].itemAmount.text = slotsWeapon[i].amount.ToString();
                slotsWeaponFast[i].SetIcon(slotsWeapon[i].item.icon);
            }
            else
            {
                slotsWeaponFast[i].SetIcon(null);
            }
        }
    }

    //Выбор предмета в одном из 6 слотов
    public void SelectSlot(int _indexSlot)
    {
        indexSlot = _indexSlot;

        for (int i = 0; i < 6; i++)
        {
            selectSlot[i].SetActive(false);
        }

        selectSlot[indexSlot - 1].SetActive(true);

    }

    //Использование предметов---------------------------------------------------------------------------
    public void UseItem(int _indexSlot)
    {
        int _index = _indexSlot - 1;

        if (slots[_index].item != null)
        {
            switch (slots[_index].item.itemType)
            {
                case ItemType.food:
                    foodItem food = (foodItem)slots[_index].item;
                    gameObject.GetComponent<ControllHealthPoint>().Recovery(food.healthAmount);
                    SubtractionItem(_index);
                    break;
            }
        }
    }

    public void SubtractionItem(int _index) //Отнять предмет
    {
        int _amount = slots[_index].amount;
        if (_amount > 1)
        {
            slots[_index].amount = slots[_index].amount-1;
            slots[_index].itemAmount.text = slots[_index].amount.ToString();
        }
        else
        {
            slots[_index].amount = 0;
            slots[_index].itemAmount.text = "";
            slots[_index].SetIcon(null);
            slots[_index].isEmpty = true;
            slots[_index].item = null;

        }
    }

}
