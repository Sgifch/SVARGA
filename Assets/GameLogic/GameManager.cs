using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public characterStat stat;
    public Transform spawnPoint;
    public GameObject player;
    public GameObject triggerZone;

    public PlayerStatManager playerStat;

    public GameObject screenDeath;
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
        player.GetComponent<inventoryManager>().LostItem();
        player.GetComponent<ControllHealthPoint>().FullRecovery();
        SaveAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        
    }

    public void ExitGeneration()
    {
        GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().DeleteStatGeneration();
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
