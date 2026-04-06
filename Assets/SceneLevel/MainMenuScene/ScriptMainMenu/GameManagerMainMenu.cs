using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMainMenu : MonoBehaviour
{
    public string nameScene;
    public Button continueButton;

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
    }

    public void PressedButtonStartOver(string name)
    {
        nameScene = name;
    }

    public void PressedButtonExit()
    {

    }

    public void PressedButtonAuthor()
    {

    }

    public void FadeAnumation()
    {

    }
    public void LoadScene()
    {

    }
}
