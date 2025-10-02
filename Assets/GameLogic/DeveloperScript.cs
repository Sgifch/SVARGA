using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperScript : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddExp()
    {
        player.GetComponent<ControllStat>().AddExp(100);
    }

    public void FullRecovery()
    {
        player.GetComponent<ControllHealthPoint>().FullRecovery();
    }

    public void UpdInterface()
    {
        KipisheFunction kipishe = GameObject.FindWithTag("Kipishe").GetComponent<KipisheFunction>();
        if (kipishe != null)
        {
            kipishe.ShowKipishe();
        }
    }

    public void Damage()
    {
        player.GetComponent<ControllHealthPoint>().Damage(10);
    }

    public void SaveInventory()
    {
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().SaveDataInventory();
    }

    public void LoadInventory()
    {
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().LoadDataInventory();
    }

    public void SaveChest()
    {
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().SaveDataChest();
    }

    public void LoadChest()
    {
        GameObject.FindWithTag("Player").GetComponent<inventoryManager>().LoadDataChest();
    }

}
