using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    public GameObject UIControll;

    private GameObject inventory;

    private GameObject inventoryFast;

    private GameObject inventoryArmor;

    private GameObject inventoryWeapon;

    private GameObject weaponFast;

    private GameObject barPanel;

    public GameObject inventoryPanel;

    public int indexSlot; 

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //Список инвентаря
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //Список быстрого инвентаря
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //возможно стоит переделать слотс ну это в будущем
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    public List<inventorySlot> slotsCopy = new List<inventorySlot>();

    private void Awake()
    {

    }
    private void Start()
    {
        UIControll = GameObject.FindWithTag("UIControll");
        inventory = UIControll.GetComponent<UIControll>().inventory;
        inventoryFast = UIControll.GetComponent<UIControll>().inventoryFast;
        weaponFast = UIControll.GetComponent<UIControll>().weaponFast;
        inventoryArmor = UIControll.GetComponent<UIControll>().inventoryArmor;
        inventoryWeapon = UIControll.GetComponent<UIControll>().inventoryWeapon;

        slots = UIControll.GetComponent<UIControll>().slots;
        slotsFast = UIControll.GetComponent<UIControll>().slotsFast;
        slotsArmor = UIControll.GetComponent<UIControll>().slotsArmor;
        slotsWeapon = UIControll.GetComponent<UIControll>().slotsWeapon;
        slotsWeaponFast = UIControll.GetComponent<UIControll>().slotsWeaponFast;
        selectSlot = UIControll.GetComponent<UIControll>().selectSlot;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionStay = true;
        this.collision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionStay = false;
    }

    public void AddItem(itemScriptableObject _item, int _amount)
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
        for (int i =0; i < inventoryFast.transform.GetChild(1).childCount; i++)
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
        for (int i = 0; i < weaponFast.transform.transform.GetChild(0).childCount; i++)
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

    public void SetSlots(List <inventorySlot> slotsList, GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount-1; i++)
        {
            if (!slotsList[i].isEmpty)
            {
                panel.transform.GetChild(i).GetComponent<inventorySlot>().item = slotsList[i].item;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().SetIcon(slotsList[i].item.icon);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().AddAmount(slotsList[i].amount);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().amount = slotsList[i].amount;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().isEmpty = false;
            }
            else
            {
                panel.transform.GetChild(i).GetComponent<inventorySlot>().item = null;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().SetIcon(null);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().AddAmount(0);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().amount = 0;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().isEmpty = true;
            }
        }
    }

    public void ClearSlots(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount - 1; i++)
        {
            if (!panel.transform.GetChild(i).GetComponent<inventorySlot>().isEmpty)
            {
                panel.transform.GetChild(i).GetComponent<inventorySlot>().item = null;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().SetIcon(null);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().AddAmount(0);
                panel.transform.GetChild(i).GetComponent<inventorySlot>().amount = 0;
                panel.transform.GetChild(i).GetComponent<inventorySlot>().isEmpty = true;
            }
        }
    }

    public void PlayerChestInventoryOpen(GameObject playerPanel)
    {
        //slotsCopy.AddRange(slots);
        SetSlots(slots, playerPanel);

    }

    public void PlayerChestInventoryClose(GameObject playerPanel)
    {
        //slots.Clear();
        //slots.AddRange(slotsCopy);

        //slotsCopy.Clear();
        //ClearSlots(playerPanel);
        SetSlots(slots, inventoryPanel);
    }

}
