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

    [Header("�������������� ��������")]
    public TMP_Text counterRoomText;
    public TMP_Text hpInf;
    public TMP_Text mannaInf;
    public TMP_Text strongInf;

    [Header ("���� �������")]
    public GameObject inventoryChest;
    public GameObject playerPanel;
    public GameObject chestPanel;

    [Header("HUD ��������")]
    public GameObject inventoryFast;
    public GameObject weaponFast;
    public GameObject HealthBarPanel;

    [Header("������ ����")]
    public GameObject upgradeMenu;
    public GameObject fontainMenu;
    public GameObject developerMenu;

    public GameObject fontain;

    [Header("���������")]
    public GameObject signPanel;
    public GameObject hintKey;

    [Header("����� ������")]
    public GameObject screenDeath;

    [Header("���� �����")]
    public GameObject paused;

    [Header("������� �����")]
    public TMP_Text damageText;

    public bool isFontain = false;


    public bool isStay = false;
    public bool isStayCollision = false;
    public bool isInventoryOpen = false;
    public bool isUpgradeMenu = false;
    public bool isOpen = false;
    private bool isOpenDev = false;
    public bool isHint = false;
    public bool isSign = false;
    public bool isChest = false;
    public Collider2D collision;

    [Header("�����")]
    public List<inventorySlot> slots = new List<inventorySlot>(); //������ ���������
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //������ �������� ���������
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //�������� ����� ���������� ����� �� ��� � �������
    public List<inventorySlot> slotsWeapon = new List<inventorySlot>();
    public List<inventorySlot> slotsWeaponFast = new List<inventorySlot>();
    public List<GameObject> selectSlot = new List<GameObject>();

    public List<inventorySlot> slotsCopy = new List<inventorySlot>();
    public List<inventorySlot> slotsChest = new List<inventorySlot>();

    private PlayerStatManager statManager;

    public enum StateUI
    {
        idle,
        inventoryOpen,
        otherMenuOpen,
        pausedOpen,
        chestInventoryOpen,
    }

    public StateUI stateUI;


    public void Awake()
    {
        statManager = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();

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

        for (int i = 0; i < playerPanel.transform.childCount; i++)
        {
            if (playerPanel.transform.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsCopy.Add(playerPanel.transform.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
            }
        }

        for (int i = 0; i < chestPanel.transform.childCount; i++)
        {
            if (chestPanel.transform.GetChild(i).GetComponent<inventorySlot>() != null) //�������� ����������
            {
                slotsChest.Add(chestPanel.transform.GetChild(i).GetComponent<inventorySlot>()); //���������� � ����
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
                inventoryChest.SetActive(false);
                informationUI.SetActive(false);
                break;

            case StateUI.otherMenuOpen:
                isStay = true;
                HintClose();
                ControllActiveHUD(false);
                break;

            case StateUI.inventoryOpen:
                isStay = true;
                HintClose();
                SignClose();
                inventory.SetActive(true);
                ControllActiveHUD(false);
                break;

            case StateUI.pausedOpen:
                ControllActiveHUD(false);
                paused.SetActive(true);
                Time.timeScale = 0; 
                break;

            case StateUI.chestInventoryOpen:
                ControllActiveHUD(false);
                HintClose();
                inventoryChest.SetActive(true);
                isStay = true;
                isChest = true;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && stateUI!=StateUI.otherMenuOpen && stateUI!=StateUI.chestInventoryOpen)
        {
            if (!isInventoryOpen)
            {
                UpgradeInventory();
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
                        print("kipishe");
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

                    case "Chest":
                        ChestMenu();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateUI = StateUI.pausedOpen; 
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

    public void UpgradeInventory()
    {
        hpInf.text = "��: " + statManager.currentHP.ToString() + "/" + statManager.currentMaxHP.ToString();
        mannaInf.text = "��: " + statManager.currentManna.ToString() + "/" + statManager.currentMaxManna.ToString();
        strongInf.text = "����: " + statManager.currentStrong.ToString();
    }

    //������-------------------------------------------------------------------------------------------------
    public void ChestMenu()
    {
        if (!isChest)
        {
            GameObject.FindWithTag("Player").GetComponent<inventoryManager>().PlayerChestInventoryOpen(playerPanel);
            stateUI = StateUI.chestInventoryOpen;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<inventoryManager>().PlayerChestInventoryClose(playerPanel);
            stateUI = StateUI.idle;
            isChest = false;
        }

    }

    //�������_��������_HUD_����-------------------------------------------------------------------------------
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

    //����������_������������_�����������-------------------------------------------------------------------
    public void HintOpen()
    {
        string tag = collision.gameObject.tag;

        if (tag == "Kipishe" || tag == "Fontain" || tag == "WeaponSpawn" || tag == "Chest")
        {
            hintKey.GetComponent<Animator>().SetTrigger("Open");
            isHint = true;
        }
    }

    public void HintClose()
    {
        if (isHint)
        {
            hintKey.GetComponent<Animator>().SetTrigger("Close");
            isHint = false;
        }
    }

    //����������_���������-----------------------------------------------------------------------------------
    public void SignOpen(string text)
    {
        if (!isSign)
        {
            TMP_Text text_sign = signPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            text_sign.text = text;
            signPanel.GetComponent<Animator>().SetTrigger("Open");
            isSign = true;
        }
    }

    public void SignClose()
    {
        if (isSign)
        {
            signPanel.GetComponent<Animator>().SetTrigger("Close");
            isSign = false;
        }
    }

    //����_�����----------------------------------------------------------------------------------------------
    public void PauseClose()
    {
        paused.SetActive(false);
        stateUI = StateUI.idle;
    }

    //��������------------------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        isStayCollision = true;
        collision = _collision;

        if (!isHint)
        {
            HintOpen();
        }

        Debug.Log(collision);
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        isStayCollision = false;
        collision = null;

        HintClose();
    }
}
