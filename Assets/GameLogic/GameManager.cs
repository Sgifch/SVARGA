using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public characterStat stat;
    public Transform spawnPoint;
    public GameObject player;
    public GameObject triggerZone;

    public PlayerStatManager playerStat;

    public GameObject screenDeath;
    public GameObject lostMenu;
    public GameObject lostContent;
    public GameObject lostSlot;

    private int counterEnemy;
    void Awake()
    {
        playerStat = GameObject.Find("PlayerStatManager").GetComponent<PlayerStatManager>();
        player = GameObject.FindWithTag("Player");

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeathGeneration()
    {
        List <inventorySlot> lostItems = player.GetComponent<inventoryManager>().LostItem();
        player.GetComponent<ControllHealthPoint>().FullRecovery();
        //SaveAll();
        LostMenu(lostItems);
        //Time.timeScale = 1;
        //SceneManager.LoadScene(1);
        
    }

    public void LostMenu(List<inventorySlot> lostItems)
    {
        screenDeath.SetActive(false);
        lostMenu.SetActive(true);
        Time.timeScale = 1;

        foreach (inventorySlot _lostItems in lostItems)
        {
            GameObject slot = Instantiate(lostSlot, transform.position, transform.rotation, lostContent.transform);
            inventorySlot _slot = slot.GetComponent<inventorySlot>();
            _slot.SetIcon(_lostItems.item.icon);
            _slot.itemAmount.text = _lostItems.amount.ToString();

        }
        
    }

    public void ExitGeneration()
    {
        GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().DeleteStatGeneration();
        player.GetComponent<inventoryManager>().LostAmulet();
        SaveAll();
        SceneManager.LoadScene(1);
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
    public void LoadGeneration()
    {
        SaveAll();
        SceneManager.LoadScene(2);
    }

    public void LoadAllInventory()
    {
        player.GetComponent<inventoryManager>().LoadDataInventory();
        player.GetComponent<inventoryManager>().LoadDataChest();
        player.GetComponent<inventoryManager>().LoadArmor();
        player.GetComponent<inventoryManager>().LoadWeapon();
    }

    public void SaveAll()
    {
        player.GetComponent<inventoryManager>().SaveDataInventory();
        player.GetComponent<inventoryManager>().SaveDataChest();
        player.GetComponent<inventoryManager>().SaveArmor();
        player.GetComponent<inventoryManager>().SaveWeapon();

        playerStat.SaveStat();

    }
}
