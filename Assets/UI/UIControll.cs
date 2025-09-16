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
    public TMP_Text counterRoomText;

    [Header("HUD элементы")]
    public GameObject inventoryFast;
    public GameObject weaponFast;
    public GameObject HealthBarPanel;

    [Header("Прочие меню")]
    public GameObject upgradeMenu;
    public GameObject fontainMenu;
    public GameObject developerMenu;

    public GameObject fontain;

    [Header("Эффекты урона")]
    public TMP_Text damageText;

    public bool isFontain = false;


    public bool isStay = false;
    public bool isStayCollision = false;
    public bool isInventoryOpen = false;
    public bool isUpgradeMenu = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    public Collider2D collision;

    [Header("Слоты")]
    public List<inventorySlot> slots = new List<inventorySlot>(); //Список инвентаря
    public List<inventorySlot> slotsFast = new List<inventorySlot>(); //Список быстрого инвентаря
    public List<inventorySlot> slotsArmor = new List<inventorySlot>(); //возможно стоит переделать слотс ну это в будущем
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
                            //проверка свободного мнста в инвентаре
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

    //Открыте закрытие HUD меню
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
