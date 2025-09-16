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
    public bool isStayCollision = false;
    public bool isInventoryOpen = false;
    public bool isUpgradeMenu = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    public Collider2D collision;

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

    }

    private void Update()
    {
        switch (stateUI)
        {
            default:
            case StateUI.idle:
                isStay = false;
                ControllActiveHUD(true);
                inventory.SetActive(false);
                informationUI.SetActive(false);
                break;

            case StateUI.otherMenuOpen:
                isStay = true;
                ControllActiveHUD(false);
                break;

            case StateUI.inventoryOpen:
                isStay = true;
                inventory.SetActive(true);
                ControllActiveHUD(false);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && stateUI!=StateUI.otherMenuOpen)
        {
            if (!isInventoryOpen)
            {
                stateUI = StateUI.inventoryOpen;
                isInventoryOpen = true;
            }
            else
            {
                stateUI = StateUI.idle;
                isInventoryOpen = false;
            }
        }

        if (collision != null && Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (collision.gameObject.tag)
                {
                    case "Kipishe":
                        UpgradeMenuOpen();
                        break;

                    case "Fontain":
                        if (!collision.gameObject.GetComponent<FontainFunction>().isTake)
                        {
                            //�������� ���������� ����� � ���������
                            if(stateUI == StateUI.idle)
                            {
                                FontainMenu();
                            }
                            
                        }
                        break;
                }
            }
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
    public void FontainMenu()
    {
        collision.gameObject.GetComponent<FontainFunction>().FontainStart();
        fontainMenu.SetActive(true);
        fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
        stateUI = StateUI.otherMenuOpen;
    }


    //��������� ���� ��� ��������� �����
    public void DamageUI(int sumDamage)
    {
        Animator anim = damageText.GetComponent<Animator>();
        damageText.text = sumDamage.ToString();
        anim.SetTrigger("Attack");
        
    }

    //���� ���������-------------------------------------------------------------------------------------------------
    public void UpgradeMenuOpen()
    {
        if (!isUpgradeMenu)
        {
            stateUI = StateUI.otherMenuOpen;
            collision.gameObject.GetComponent<KipisheFunction>().ShowKipishe();
            Animator anim = upgradeMenu.GetComponent<Animator>();
            ControllActiveOtherMenu(upgradeMenu, true);
            anim.SetTrigger("Open");

            isUpgradeMenu = true;
        }
        else
        {
            isUpgradeMenu = false;
            //ControllActiveOtherMenu(upgradeMenu, false);
            Animator anim = upgradeMenu.GetComponent<Animator>();
            anim.SetTrigger("Close");
            stateUI = StateUI.idle;
        }
 
    }

    public void UpgradeMenuClose()
    {

        if (upgradeMenu != null)
        {
            //ControllActiveOtherMenu(upgradeMenu, false);
            Animator anim = upgradeMenu.GetComponent<Animator>();
            anim.SetTrigger("Close");
            isOpen = false;
        }
        stateUI = StateUI.idle;
        collision = null;
    }

    //���������--------------------------------------------------------------------------------------------------------
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
        isStayCollision = true;
        collision = _collision;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        isStayCollision = false;
        collision = null;
    }
}
