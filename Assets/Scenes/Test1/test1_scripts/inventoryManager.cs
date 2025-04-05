using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject UIPanelFast;
    public Transform inventoryPanel;
    public Transform inventoryPanelFast;
    public bool isOpened;

    bool collisionStay = false;
    Collider2D collision = null;

    public List<inventorySlot> slots = new List<inventorySlot>(); //������ ���������
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //������ �������� ���������

    void Start()
    {
        UIPanel.SetActive(false);

        for (int i=0; i < inventoryPanel.childCount; i++)
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //�������� ���� � �������� ��������� �������� �������
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanelFast.SetActive(false);
                UIPanel.SetActive(true);
            }
            else
            {
                UIPanelFast.SetActive(true);
                UIPanel.SetActive(false);
            }
        }

        if (collisionStay) //����������� �������
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

        FastSlots(); //����������� ��������� �� ������ 6 ������
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

    private void FastSlots() //����������� ��������� �� ������ 6 ������
    {
        for (int i =0; i < inventoryPanelFast.childCount; i++)
        {
            if (slots[i].isEmpty == false)
            {
                slotsFast[i].itemAmount.text = slots[i].amount.ToString();
                slotsFast[i].SetIcon(slots[i].item.icon);
            }   
        }
    }

}
