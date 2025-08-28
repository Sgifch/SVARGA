using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControll : MonoBehaviour
{
    [Header("Инвентарь")]
    public GameObject informationUI;
    public TMP_Text counterRoomText;

    [Header("HUD элементы")]
    public GameObject inventoryFast;
    public GameObject WeaponFastPanel;
    public GameObject HealthBarPanel;

    [Header("Прочие меню")]
    public GameObject upgradeMenu;
    public GameObject developerMenu;

    [Header("Эффекты урона")]
    public TMP_Text damageText;

    private bool isStay = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    private Collider2D collision;

    //Вот это всё потом переделать под отделльный элемент на сцене

    private void Update()
    {
        

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
        WeaponFastPanel.SetActive(active);
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
