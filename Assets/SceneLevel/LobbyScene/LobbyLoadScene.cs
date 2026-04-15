using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLoadScene : MonoBehaviour
{
    public string sceneName;
    public GameManager gameManager;
    public GameObject changeCanvas;
    
    public void Load()
    {
        gameManager.SaveAll();
        SceneManager.LoadScene(sceneName);
    }

    public void DisActivate()
    {
        changeCanvas.SetActive(false);
    }
}
