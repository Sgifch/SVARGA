using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    [Header("HUD элементы")]
    public GameObject inventoryFast;
    public GameObject WeaponFastPanel;
    public GameObject HealthBarPanel;

    [Header("Прочие меню")]
    public GameObject upgradeMenu;
    public GameObject developerMenu;

    private bool isStay = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    private Collider2D collision;

    private void Update()
    {
        if (isStay)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen)
                {
                    switch (collision.gameObject.tag)
                    {
                        case "Kipishe":
                            ControllActiveHUD(false);
                            ControllActiveOtherMenu(upgradeMenu, true);
                            break;
                    }
                    isOpen = true;
                }
                else
                {
                    isOpen = false;
                    ControllActiveHUD(true);
                    ControllActiveOtherMenu(upgradeMenu, false);
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
        isStay = false;
        ControllActiveHUD(true);
        ControllActiveOtherMenu(upgradeMenu, false);
        collision = null;
    }
}
