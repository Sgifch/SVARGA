using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControll : MonoBehaviour
{
    [Header("���������")]
    public GameObject informationUI;
    public TMP_Text counterRoomText;

    [Header("HUD ��������")]
    public GameObject inventoryFast;
    public GameObject WeaponFastPanel;
    public GameObject HealthBarPanel;

    [Header("������ ����")]
    public GameObject upgradeMenu;
    public GameObject fontainMenu;
    public GameObject developerMenu;

    [Header("������� �����")]
    public TMP_Text damageText;

    public bool isFontain = false;


    public bool isStay = false;
    public bool isOpen = false;
    private bool isOpenDev = false; 
    private Collider2D collision;

    public enum StateUI
    {
        idle,
        inventoryOpen,
        otherMenuOpen,
    }

    private StateUI stateUI;

    //��� ��� �� ����� ���������� ��� ���������� ������� �� �����

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
    public void FontainMenu()
    {
        if (!isFontain)
        {
            fontainMenu.SetActive(true);
            isFontain = true;
            fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
            stateUI = StateUI.otherMenuOpen;
        }
        else
        {
            stateUI = StateUI.idle;
            fontainMenu.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
            //fontainMenu.SetActive(false);
            isFontain = false;
            gameObject.GetComponent<inventoryManager>().isOpened = false; //��������
        }
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
