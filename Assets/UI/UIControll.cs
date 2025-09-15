using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControll : MonoBehaviour
{
    [Header("���������")]
    public GameObject inventory;
    public GameObject inventoryArmor;
    public GameObject inventoryWeapon;
    public GameObject informationUI;
    public TMP_Text counterRoomText;

    [Header("HUD ��������")]
    public GameObject inventoryFast;
    public GameObject weaponFast;
    public GameObject HealthBarPanel;

    [Header("������ ����")]
    public GameObject upgradeMenu;
    public GameObject fontainMenu;
    public GameObject developerMenu;

    public GameObject fontain;

    [Header("������� �����")]
    public TMP_Text damageText;

    public bool isFontain = false;


    public bool isStay = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    private Collider2D collision;

    [Header("�����")]
    public List<inventorySlot> slots = new List<inventorySlot>(); //������ ���������
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //������ �������� ���������
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //�������� ����� ���������� ����� �� ��� � �������
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    public enum StateUI
    {
        idle,
        inventoryOpen,
        otherMenuOpen,
    }

    public StateUI stateUI;

    //��� ��� �� ����� ���������� ��� ���������� ������� �� �����

    public void Awake()
    {
        for (int i = 0; i < inventory.transform.GetChild(0).GetChild(0).childCount; i++) //�� ���� ���� ����� �������� � ���� ������� ��� ��������
        {
            if (inventory.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slots.Add(inventory.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryFast.transform.GetChild(1).childCount; i++)
        {
            if (inventoryFast.transform.GetChild(1).GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsFast.Add(inventoryFast.transform.GetChild(1).GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryArmor.transform.childCount; i++)
        {
            if (inventoryArmor.transform.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsArmor.Add(inventoryArmor.transform.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryWeapon.transform.childCount; i++)
        {
            if (inventoryWeapon.transform.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsWeapon.Add(inventoryWeapon.transform.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        //����� ��� ����������� ������
        for (int i = 0; i < weaponFast.transform.GetChild(0).childCount; i++)
        {
            if (weaponFast.transform.GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsWeaponFast.Add(weaponFast.transform.GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < inventoryFast.transform.GetChild(1).childCount; i++)
        {
            selectSlot.Add(inventoryFast.transform.GetChild(1).GetChild(i).GetChild(1).gameObject);
        }

        //��������, ����������� �� ��������� ����
        /*for (int i = 0; i < 6; i++)
        {

            selectSlot[i].SetActive(false);
        }

        selectSlot[0].SetActive(true);
        SelectSlot(1);

        inventory.SetActive(false);*/
    }

    private void Update()
    {
        switch (stateUI)
        {
            default:
            case StateUI.idle:
                isStay = false;
                ControllActiveHUD(true);
                break;

            case StateUI.otherMenuOpen:
                isStay = true;
                ControllActiveHUD(false);
                //�������� ���������
                break;

            case StateUI.inventoryOpen:
                isStay = true;
                break;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!isOpenDev)
            {
                ControllActiveHUD(false);
                developerMenu.SetActive(true);
                isOpenDev = true;
            }
            else
            {
                developerMenu.SetActive(false);
                ControllActiveHUD(true);
                isOpenDev = false;
            }
        }

    }
    public void FontainMenu(GameObject _fontain)
    {
        if (!isFontain)
        {
            fontain = _fontain;
            fontain.GetComponent<FontainFunction>().FontainStart();
            fontainMenu.SetActive(true);
            isFontain = true;
            fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
            stateUI = StateUI.otherMenuOpen;
        }
        /*else
        {
            stateUI = StateUI.idle;
            fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
            //fontainMenu.SetActive(false);
            isFontain = false;
            gameObject.GetComponent<inventoryManager>().isOpened = false; //��������
            fontain.GetComponent<FontainFunction>().FontainEnd();
        }*/
    }


    //��������� ���� ��� ��������� �����
    public void DamageUI(int sumDamage)
    {
        Animator anim = damageText.GetComponent<Animator>();
        damageText.text = sumDamage.ToString();
        anim.SetTrigger("Attack");
        
    }

    //�������� �������� ���� ���������
    public void UpgradeMenu()
    {
        if (!isOpen)
        {
            ControllActiveHUD(false);
            collision.gameObject.GetComponent<KipisheFunction>().ShowKipishe();
            Animator anim = upgradeMenu.GetComponent<Animator>();
            ControllActiveOtherMenu(upgradeMenu, true);
            anim.SetTrigger("Open");
            
            isOpen = true;
        }
        else
        {
            isOpen = false;
            ControllActiveHUD(true);
            
            ControllActiveOtherMenu(upgradeMenu, false);
        }
    }

    //������� ���� ���������
    public void UpgradeMenuClose()
    {
        ControllActiveHUD(true);
        if (upgradeMenu != null)
        {
            ControllActiveOtherMenu(upgradeMenu, false);
            isOpen = false;
        }
        collision = null;
    }

    public void InventoryClose()
    {
        if (informationUI != null)
        {
            informationUI.SetActive(false);
        }
    }

    public void InformationUIOpen()
    {
        counterRoomText.text = GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().counterRoom.ToString();
        informationUI.SetActive(true);
    }

    //������� �������� HUD ����
    public void ControllActiveHUD(bool active)
    {
        inventoryFast.SetActive(active);
        weaponFast.SetActive(active);
        HealthBarPanel.SetActive(active);
    }

    public void ControllActiveOtherMenu(GameObject menu, bool active)
    {
        menu.SetActive(active);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        isStay = true;
        collision = _collision;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {

    }
}
