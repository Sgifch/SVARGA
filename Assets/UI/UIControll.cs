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
    public bool isOpen = false;
    private bool isOpenDev = false; 
    private Collider2D collision;

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

    //Вот это всё потом переделать под отделльный элемент на сцене

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

        //Слоты для отображения оружия
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

        //Картинки, указывающие на выбранный слот
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
                //закрытие инвентаря
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
            gameObject.GetComponent<inventoryManager>().isOpened = false; //временно
            fontain.GetComponent<FontainFunction>().FontainEnd();
        }*/
    }


    //Появление цифр при нанесении урона
    public void DamageUI(int sumDamage)
    {
        Animator anim = damageText.GetComponent<Animator>();
        damageText.text = sumDamage.ToString();
        anim.SetTrigger("Attack");
        
    }

    //Открытие закрытие меню улучшения
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

    //Закрыть меню улучшения
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
        isStay = true;
        collision = _collision;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {

    }
}
