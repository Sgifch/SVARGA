using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManagerMainMenu : MonoBehaviour
{
    public string nameScene;
    public Button continueButton;
    public GameObject blackout;
    
    private string _fileNameInventory = "inventoryDataFile";
    private string _fileNameChest = "chestDataFile";
    private string _fileNameArmor = "armorDataFile";
    private string _fileNameWeapon = "weaponDataFile";

    private void Start()
    {
        //Проверка есть ли сохранения
        if (!PlayerPrefs.HasKey("maxHP"))
        {
            continueButton.interactable = false;
        }
    }
    public void PressedButtonContinue(string name)
    {
        nameScene = name;
        blackout.SetActive(true);

    }

    public void PressedButtonStartOver(string name)
    {
        nameScene = name;
        if (PlayerPrefs.HasKey("maxHP"))
        {
            PlayerPrefs.DeleteAll();
            string filePath1 = Application.persistentDataPath + "/" + _fileNameInventory;
            if (File.Exists(filePath1))
            {
                File.Delete(filePath1);
            }
            string filePath2 = Application.persistentDataPath + "/" + _fileNameArmor;
            if (File.Exists(filePath2))
            {
                File.Delete(filePath2);
            }
            string filePath3 = Application.persistentDataPath + "/" + _fileNameWeapon;
            if (File.Exists(filePath3))
            {
                File.Delete(filePath3);
            }
            string filePath4 = Application.persistentDataPath + "/" + _fileNameChest;
            if (File.Exists(filePath4))
            {
                File.Delete(filePath4);
            }
            blackout.SetActive(true);
        }
        else
        {
            blackout.SetActive(true);
        }
    }

    public void PressedButtonExit()
    {
        Application.Quit();
    }

    public void PressedButtonAuthor()
    {

    }

    /*public void FadeAnumation()
    {

    }
    public void LoadScene()
    {

    }*/
}
