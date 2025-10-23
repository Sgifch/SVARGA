using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    public GameObject UIControll;

    private GameObject inventory;

    private GameObject inventoryFast;

    private GameObject panelArmor;

    private GameObject panelWeapon;

    private GameObject weaponFast;

    private GameObject barPanel;

    public GameObject inventoryPanel;

    public int indexSlot; 

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //������ ���������
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //������ �������� ���������
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //�������� ����� ���������� ����� �� ��� � �������
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    public List<inventorySlot> slotsCopy = new List<inventorySlot>();
    public List<inventorySlot> slotsChest = new List<inventorySlot>();

    [Header("���� ������ ��������")]
    public float lostChance;

    [Header("�������� ������ ����������")]
    public string _fileNameInventory;
    public string _fileNameChest;
    public string _fileNameArmor;
    public string _fileNameWeapon;
    private void Awake()
    {

    }
    private void Start()
    {
        UIControll = GameObject.FindWithTag("UIControll");
        inventory = UIControll.GetComponent<UIControll>().inventory;
        inventoryFast = UIControll.GetComponent<UIControll>().inventoryFast;
        weaponFast = UIControll.GetComponent<UIControll>().weaponFast;
        panelArmor = UIControll.GetComponent<UIControll>().inventoryArmor;
        panelWeapon = UIControll.GetComponent<UIControll>().inventoryWeapon;

        slots = UIControll.GetComponent<UIControll>().slots;
        slotsFast = UIControll.GetComponent<UIControll>().slotsFast;
        slotsArmor = UIControll.GetComponent<UIControll>().slotsArmor;
        slotsWeapon = UIControll.GetComponent<UIControll>().slotsWeapon;
        slotsWeaponFast = UIControll.GetComponent<UIControll>().slotsWeaponFast;
        selectSlot = UIControll.GetComponent<UIControll>().selectSlot;
        slotsCopy = UIControll.GetComponent<UIControll>().slotsCopy;
        slotsChest = UIControll.GetComponent<UIControll>().slotsChest;


        //��������, ����������� �� ��������� ����
        for (int i = 0; i < 6; i++)
        {

            selectSlot[i].SetActive(false);
        }

        selectSlot[0].SetActive(true);
        SelectSlot(1);

        inventory.SetActive(false);

        LoadDataInventory();
        LoadDataChest();
        LoadArmor();
        LoadWeapon();

    }

    void Update()
    {

        if (collisionStay) //����������� �������
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collision.gameObject.tag == "item") //���������� ��� switchCase
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

        //����� ������ �� 6 ������
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

        CopySlots(); //����������� ��������� �� ������ 6 ������
                     //����� �� ������ � ���� �����������������
                     //��� ����� ������ ��� ����������� ���������

    }

    //����������-��������-���������--------------------------------------------------------------------------
    public void SaveDataInventory() //���������
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileNameInventory);
        for (int i = 0; i<slots.Count; i++)
        {
            InventoryDataSlot newSlot = new(slots[i]);
            string json = JsonUtility.ToJson(newSlot);
            sw.WriteLine(json);
        }
        sw.Close();
    }

    public void LoadDataInventory()
    {
        if(File.Exists(Application.persistentDataPath + "/" + _fileNameInventory))
        {
            string[] readed = File.ReadAllLines(Application.persistentDataPath + "/" + _fileNameInventory);
            for (int i = 0; i < readed.Length; i++)
            {
                InventoryDataSlot saveSlot;
                saveSlot = JsonUtility.FromJson<InventoryDataSlot>(readed[i]);
                slots[i].item = saveSlot.item;
                slots[i].amount = saveSlot.amount;
                slots[i].isEmpty = saveSlot.isEmpty;
            }

            SetSlots(slots, inventoryPanel);
        }
    }

    public void SaveArmor() //�����
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileNameArmor);
        for (int i = 0; i < slotsArmor.Count; i++)
        {
            InventoryDataSlot newSlot = new(slotsArmor[i]);
            string json = JsonUtility.ToJson(newSlot);
            sw.WriteLine(json);
        }
        sw.Close();
    }

    public void LoadArmor()
    {
        if (File.Exists(Application.persistentDataPath + "/" + _fileNameArmor))
        {
            string[] readed = File.ReadAllLines(Application.persistentDataPath + "/" + _fileNameArmor);
            for (int i = 0; i < readed.Length; i++)
            {
                InventoryDataSlot saveSlot;
                saveSlot = JsonUtility.FromJson<InventoryDataSlot>(readed[i]);
                slotsArmor[i].item = saveSlot.item;
                slotsArmor[i].amount = saveSlot.amount;
                slotsArmor[i].isEmpty = saveSlot.isEmpty;
            }

            SetSlots(slotsArmor, panelArmor);
        }
    }

    public void SaveWeapon()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileNameWeapon);
        for (int i = 0; i < slotsWeapon.Count; i++)
        {
            InventoryDataSlot newSlot = new(slotsWeapon[i]);
            string json = JsonUtility.ToJson(newSlot);
            sw.WriteLine(json);
        }
        sw.Close();
    }

    public void LoadWeapon()
    {
        if (File.Exists(Application.persistentDataPath + "/" + _fileNameWeapon))
        {
            string[] readed = File.ReadAllLines(Application.persistentDataPath + "/" + _fileNameWeapon);
            for (int i = 0; i < readed.Length; i++)
            {
                InventoryDataSlot saveSlot;
                saveSlot = JsonUtility.FromJson<InventoryDataSlot>(readed[i]);
                slotsWeapon[i].item = saveSlot.item;
                slotsWeapon[i].amount = saveSlot.amount;
                slotsWeapon[i].isEmpty = saveSlot.isEmpty;
            }

            SetSlots(slotsWeapon, panelWeapon);
        }
    }

    //����������-��������-�������-----------------------------------------------------------------------
    public void SaveDataChest()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileNameChest);
        for (int i = 0; i < slotsChest.Count; i++)
        {
            InventoryDataSlot newSlot = new(slotsChest[i]);
            string json = JsonUtility.ToJson(newSlot);
            sw.WriteLine(json);
        }
        sw.Close();
    }

    public void LoadDataChest()
    {
        if (File.Exists(Application.persistentDataPath + "/" + _fileNameChest))
        {
            string[] readed = File.ReadAllLines(Application.persistentDataPath + "/" + _fileNameChest);
            for (int i = 0; i < readed.Length; i++)
            {
                InventoryDataSlot saveSlot;
                saveSlot = JsonUtility.FromJson<InventoryDataSlot>(readed[i]);
                slotsChest[i].item = saveSlot.item;
                slotsChest[i].amount = saveSlot.amount;
                slotsChest[i].isEmpty = saveSlot.isEmpty;
            }

            SetSlots(slotsChest, GameObject.FindWithTag("UIControll").GetComponent<UIControll>().chestPanel);
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

    //����������_��������------------------------------------------------------------------
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

    //�����������-������------------------------------------------------------------------------
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

        //��� ����� ������ � ������������ ������ ����� ����� ����������
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

    //�����-��������-�-�����-��-6-������---------------------------------------------------------------
    public void SelectSlot(int _indexSlot)
    {
        indexSlot = _indexSlot;

        for (int i = 0; i < 6; i++)
        {
            selectSlot[i].SetActive(false);
        }

        selectSlot[indexSlot - 1].SetActive(true);

    }

    //������������� ���������---------------------------------------------------------------------------
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

                case ItemType.foodDuration:
                    FoodItemDuration foodDuration = (FoodItemDuration)slots[_index].item;
                    gameObject.GetComponent<ControllHealthPoint>().DurationRecovery(foodDuration.healthAmount, foodDuration.time, foodDuration.interval);
                    SubtractionItem(_index);
                    break;

                case ItemType.unFood:
                    UnFoodItem unFood = (UnFoodItem)slots[_index].item;
                    gameObject.GetComponent<ControllHealthPoint>().Damage(unFood.unHealthAmount);
                    SubtractionItem(_index);
                    break;

                case ItemType.unFoodDuration:
                    UnFoodItemDuration unFoodDuration = (UnFoodItemDuration)slots[_index].item;
                    gameObject.GetComponent<ControllHealthPoint>().DurationDamage(unFoodDuration.unHealthAmount, unFoodDuration.time, unFoodDuration.interval);
                    SubtractionItem(_index);
                    break;
            }
        }
    }

    //������-��������-----------------------------------------------------------------------------------
    public void SubtractionItem(int _index)
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

    //������-���������------------------------------------------------------------------------------------
    public void LostItem()
    {
        foreach (inventorySlot _slots in slots)
        {
            if (!_slots.isEmpty)
            {
                float rnd = Random.Range(0, 1);
                if (rnd <= lostChance)
                {
                    if (_slots.amount == 1)
                    {
                        DestroyItem(_slots);
                    }
                    else
                    {
                        int rndAmount = Random.Range(1, _slots.amount);

                        if(rndAmount == _slots.amount)
                        {
                            DestroyItem(_slots);
                        }
                        else
                        {
                            _slots.amount = _slots.amount - rndAmount;
                        }
                    }
                }
            }

        }
    }

    public void LostAmulet() //��������-����-��������-------------------------------------------------------------------------
    {
        foreach(inventorySlot _slots in slots)
        {
            if (!_slots.isEmpty)
            {
                if (_slots.item.itemType == ItemType.amulet)
                {
                    DestroyItem(_slots);
                }
            }
        }

        foreach(inventorySlot _slots in slotsArmor)
        {
            if (!_slots.isEmpty)
            {
                DestroyItem(_slots);
                _slots.GetComponent<EquipmentInventory>().UnequipmentAmulet();
            }
        }
    }

    public void DestroyItem(inventorySlot _slots) //����������� �������� ���� �� ������� ����
    {
        _slots.amount = 0;
        _slots.itemAmount.text = "";
        _slots.SetIcon(null);
        _slots.isEmpty = true;
        _slots.item = null;
    }

    //��������-�������������-���������-----------------------------------------------------------------
    public bool FullInventory()
    {
        bool empty = false;

        foreach(inventorySlot _slots in slots)
        {
            if (_slots.isEmpty)
            {
                empty = true;
                break;
            }
            
        }

        return empty;
    }

    //����������-������-----------------------------------------------------------------------------------
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
        SetSlots(slots, playerPanel);

    }

    public void PlayerChestInventoryClose(GameObject playerPanel)
    {
        SetSlots(slotsCopy, inventoryPanel);
    }

}
