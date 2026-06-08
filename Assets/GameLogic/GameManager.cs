using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public characterStat stat;
    public Transform spawnPoint;
    public GameObject player;
    public GameObject triggerZone;

    public PlayerStatManager playerStat;
    private inventoryManager inventory;

    public GameObject screenDeath;
    public GameObject lostMenu;
    public GameObject lostContent;
    public GameObject lostSlot;
    public GameObject dash;
    public GameObject endMenu;
    public Button infoButton;

    private int counterEnemy;

    //Анимация-перехода
    public Animator animChanger;
    public int n;

    //Управление-лобби
    public bool isLobby;

    public GameObject fire1;
    public GameObject fire2;
    public GameObject trigger;
    public GameObject firefly;

    public GameObject canvasChange;
    public GameObject blackout;


    void Awake()
    {
        playerStat = GameObject.Find("PlayerStatManager").GetComponent<PlayerStatManager>();
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<inventoryManager>();

        if (PlayerPrefs.HasKey("maxHP"))
        {
            playerStat.LoadStat();
        }
        screenDeath = GameObject.FindWithTag("UIControll").GetComponent<UIControll>().screenDeath;
        //player.GetComponent<ControllHealthPoint>().ChangeHealthBar();
    }   

    private void Start()
    {
        //LoadAllInventory();
        //GameObject.FindWithTag("UIControl");
        if (SceneManager.GetActiveScene().name != "GenerationScene")
        {
            infoButton.interactable = false;
        }

        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            animChanger.SetTrigger("Blackin");
        }

        if (isLobby)
        {
            ActivateEndRoom();
        }
    }

    void Update()
    {
        if (playerStat.currentHP <= 0)
        {
            Death();
        }
    }

    //Смерти----------------------------------------------------------------------
    public void Death()
    {
        screenDeath.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        player.GetComponent<ControllHealthPoint>().FullRecovery();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGeneration()
    {
        SaveAll();

        /*inventory.SaveDataInventory(inventory._fileNameInventoryGen);
        inventory.SaveDataChest();
        inventory.SaveArmor();
        inventory.SaveWeapon(inventory._fileNameWeaponGen);

        playerStat.SaveStat();*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeathGeneration()
    {
        player.GetComponent<inventoryManager>().LostAmulet();
        List <inventorySlot> lostItems = player.GetComponent<inventoryManager>().LostItem();
        player.GetComponent<ControllHealthPoint>().FullRecovery();
        //SaveAll();
        LostMenu(lostItems);
        //SaveAll();
        //Time.timeScale = 1;
        //SceneManager.LoadScene(1);
        
    }

    //Проигрыш--------------------------------------------------------------------------
    public void LostMenu(List<inventorySlot> lostItems)
    {
        screenDeath.SetActive(false);
        Time.timeScale = 1;
        lostMenu.SetActive(true);
        dash.SetActive(false);
        print(lostItems.Count);
        if (lostItems.Count > 0)
        {
            foreach (inventorySlot _lostItems in lostItems)
            {
                GameObject slot = Instantiate(lostSlot, transform.position, transform.rotation, lostContent.transform);
                inventorySlot _slot = slot.GetComponent<inventorySlot>();
                _slot.SetIcon(_lostItems.item.icon);
                _slot.itemAmount.text = _lostItems.amount.ToString();

            }
        }
        else
        {
            dash.SetActive(true);
        }
        //SaveAll();
        Time.timeScale = 0;
    }

    //Выйгрыш---------------------------------------------------------------------------
    public void EndMenu()
    {
        endMenu.SetActive(true);
    }

    //Выход-в-лобби---------------------------------------------------------------------
    public void ExitGeneration()
    {
        Time.timeScale = 1;
        GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().DeleteStatGeneration();
        player.GetComponent<inventoryManager>().LostAmulet();
        EndSave();
        canvasChange.SetActive(true);
        blackout.GetComponent<LobbyLoadScene>().sceneName = "Lobby";
        blackout.GetComponent<Animator>().SetTrigger("Blackout");
    }

    public void ActivateTrigger()
    {
        counterEnemy++;
        if (counterEnemy == 3)
        {
            triggerZone.GetComponent<TriggerEvent>().TriggerEventEnd();
        }
    }

    //Загрузки-сцен----------------------------------------------------------------------------------------------
    /*public void LoadGeneration()
    {
        SaveAll();
        SceneManager.LoadScene(2);
    }*/

    /*public void LoadAllInventory()
    {

        inventory.LoadDataInventory();
        inventory.LoadDataChest();
        inventory.LoadArmor();
        inventory.LoadWeapon();
    }*/

    public void SaveAll()
    {
        if (SceneManager.GetActiveScene().name != "GenerationScene")
        {
            inventory.SaveDataInventory(inventory._fileNameInventory);
            inventory.SaveDataChest();
            inventory.SaveArmor();
            inventory.SaveWeapon(inventory._fileNameWeapon);

            playerStat.SaveStat();
        }

    }

    public void EndSave()
    {
        inventory.SaveDataInventory(inventory._fileNameInventory);
        inventory.SaveDataChest();
        inventory.SaveArmor();
        inventory.SaveWeapon(inventory._fileNameWeapon);

        playerStat.SaveStat();
    }

    public void SaveRestart()
    {
        inventory.SaveDataInventory(inventory._fileNameInventoryGen);
        inventory.SaveDataChest();
        inventory.SaveArmor();
        inventory.SaveWeapon(inventory._fileNameWeaponGen);

        playerStat.SaveStat();
    }

    //Управление-лобби
    public void ActivateEndRoom()
    {
        if (playerStat.readySouls == 1)
        {
            fire1.SetActive(true);
            fire2.SetActive(true);
            trigger.SetActive(true);

            if (playerStat.firstReadySouls == 0)
            {
                firefly.SetActive(true);
                playerStat.firstReadySouls = 1;
                //PlayerPrefs.GetInt("firstReadySouls", 1);
            }
        }
    }
}
