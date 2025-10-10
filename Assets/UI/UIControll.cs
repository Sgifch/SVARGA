using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControll : MonoBehaviour
{
    [Header("Инвентарь")]
    public GameObject inventory;
    public GameObject inventoryArmor;
    public GameObject inventoryWeapon;
    public GameObject informationUI;

    [Header("Информационные элементы")]
    public TMP_Text counterRoomText;
    public TMP_Text hpInf;
    public TMP_Text mannaInf;
    public TMP_Text strongInf;

    [Header ("Меню сундука")]
    public GameObject inventoryChest;
    public GameObject playerPanel;
    public GameObject chestPanel;

    [Header("HUD элементы")]
    public GameObject inventoryFast;
    public GameObject weaponFast;
    public GameObject HealthBarPanel;

    [Header("Прочие меню")]
    public GameObject upgradeMenu;
    public GameObject fontainMenu;
    public GameObject developerMenu;

    public GameObject fontain;

    [Header("Подсказки")]
    public GameObject signPanel;
    public GameObject hintKey;

    [Header("Экран смерти")]
    public GameObject screenDeath;

    [Header("Меню паузы")]
    public GameObject paused;

    [Header("Эффекты урона")]
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

    [Header("Слоты")]
    public List<inventorySlot> slots = new List<inventorySlot>(); //Список инвентаря
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //Список быстрого инвентаря
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //возможно стоит переделать слотс ну это в будущем
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

        for (int i = 0; i < inventory.transform.GetChild(0).GetChild(0).childCount; i++) //мб этот блок потом засунуть в одну функцию для удобства
        {
            if (inventory.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slots.Add(inventory.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryFast.transform.GetChild(1).childCount; i++)
        {
            if (inventoryFast.transform.GetChild(1).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsFast.Add(inventoryFast.transform.GetChild(1).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryArmor.transform.childCount; i++)
        {
            if (inventoryArmor.transform.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsArmor.Add(inventoryArmor.transform.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < inventoryWeapon.transform.childCount; i++)
        {
            if (inventoryWeapon.transform.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsWeapon.Add(inventoryWeapon.transform.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < weaponFast.transform.GetChild(0).childCount; i++)
        {
            if (weaponFast.transform.GetChild(0).GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsWeaponFast.Add(weaponFast.transform.GetChild(0).GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < playerPanel.transform.childCount; i++)
        {
            if (playerPanel.transform.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsCopy.Add(playerPanel.transform.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }

        for (int i = 0; i < chestPanel.transform.childCount; i++)
        {
            if (chestPanel.transform.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slotsChest.Add(chestPanel.transform.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
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
                            //проверка свободного мнста в инвентаре
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


    //Появление цифр при нанесении урона
    public void DamageUI(int sumDamage)
    {
        Animator anim = damageText.GetComponent<Animator>();
        damageText.text = sumDamage.ToString();
        anim.SetTrigger("Attack");
        
    }

    //Меню улучшений-------------------------------------------------------------------------------------------------
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

    //Инвентарь--------------------------------------------------------------------------------------------------------
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
        hpInf.text = "ОЗ: " + statManager.currentHP.ToString() + "/" + statManager.currentMaxHP.ToString();
        mannaInf.text = "ОМ: " + statManager.currentManna.ToString() + "/" + statManager.currentMaxManna.ToString();
        strongInf.text = "Сила: " + statManager.currentStrong.ToString();
    }

    //Сундук-------------------------------------------------------------------------------------------------
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

    //Открыте_закрытие_HUD_меню-------------------------------------------------------------------------------
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

    //Управление_всплывающими_подсказками-------------------------------------------------------------------
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

    //Управление_табличкой-----------------------------------------------------------------------------------
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

    //Меню_паузы----------------------------------------------------------------------------------------------
    public void PauseClose()
    {
        paused.SetActive(false);
        stateUI = StateUI.idle;
    }

    //Триггеры------------------------------------------------------------------------------------------------
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
