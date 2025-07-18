using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject weaponFastPanel;
    public Transform _weaponFastPanel;

    public Transform panelSelectSlot;

    public GameObject barPanel;

    public MainCharacterControllerW mainCharacter;

    public bool isOpened;

    public int indexSlot; //������ ���������� �����

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //������ ���������
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //������ �������� ���������
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //�������� ����� ���������� ����� �� ��� � �������
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    private void Awake()
    {
        weaponFastPanel = GameObject.Find("WeaponFastPanel");
        _weaponFastPanel = weaponFastPanel.transform.GetChild(0).GetComponent<Transform>();
    }
    void Start()
    {
      

        for (int i=0; i < inventoryPanel.childCount; i++) //�� ���� ���� ����� �������� � ���� ������� ��� ��������
        {
            if (inventoryPanel.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i=0; i < inventoryPanelFast.childCount; i++)
        {
            if (inventoryPanelFast.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsFast.Add(inventoryPanelFast.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryArmor.childCount; i++)
        {
            if (inventoryArmor.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsArmor.Add(inventoryArmor.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryWeapon.childCount; i++)
        {
            if (inventoryWeapon.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsWeapon.Add(inventoryWeapon.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        //����� ��� ����������� ������
        for (int i = 0; i < _weaponFastPanel.childCount; i++)
        {
            if (_weaponFastPanel.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsWeaponFast.Add(_weaponFastPanel.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        //��������, ����������� �� ��������� ����
        for (int i = 0; i < 6; i++)
        {

            selectSlot[i].SetActive(false);
        }

        selectSlot[0].SetActive(true);
        SelectSlot(1);

        UIPanel.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //�������� ���� � �������� ��������� �������� �������
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanelFast.SetActive(false);
                weaponFastPanel.SetActive(false);
                UIPanel.SetActive(true);
                barPanel.SetActive(false);
            }
            else
            {
                UIPanelFast.SetActive(true);
                weaponFastPanel.SetActive(true);
                UIPanel.SetActive(false);
                barPanel.SetActive(true);
            }
        }

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

    private void CopySlots() //����������� ��������� �� ������ 6 ������
    {
        for (int i =0; i < inventoryPanelFast.childCount; i++) //������ ����� ��� ���� �������� � ��� ����� ���� ������� ������
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
        for (int i = 0; i < _weaponFastPanel.childCount; i++)
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

    //����� �������� � ����� �� 6 ������
    public void SelectSlot(int _indexSlot)
    {
        indexSlot = _indexSlot;

        for (int i = 0; i < 6; i++)
        {
            selectSlot[i].SetActive(false);
        }

        selectSlot[indexSlot - 1].SetActive(true);

    }

    //������������� ��������
    public void UseItem(int _indexSlot)
    {
        int _index = _indexSlot - 1;

        if (slots[_index].item != null)
        {
            if (slots[_index].item.itemType is ItemType.food) //���� ������ ��� (������� ����� switch)
            {
                mainCharacter.UseFood(slots[_index]);
                SubtractionItem(_index);

            }
        }
    }

    public void SubtractionItem(int _index) //������ �������
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
