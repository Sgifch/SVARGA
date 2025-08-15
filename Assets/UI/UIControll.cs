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

    private bool isStay = false;
    private bool isOpen = false;
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
                            ControllActiveOtherMenu(true);
                            break;
                    }
                    isOpen = true;
                }
                else
                {
                    isOpen = false;
                    ControllActiveHUD(true);
                    ControllActiveOtherMenu(false);
                }
            }
        }
    }
    public void ControllActiveHUD(bool active)
    {
        inventoryFast.SetActive(active);
        WeaponFastPanel.SetActive(active);
        HealthBarPanel.SetActive(active);
    }

    public void ControllActiveOtherMenu(bool active)
    {
        upgradeMenu.SetActive(active);
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
        ControllActiveOtherMenu(false);
        collision = null;
    }
}
