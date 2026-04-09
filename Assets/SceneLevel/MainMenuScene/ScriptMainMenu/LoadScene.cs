using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject gameManager;
    public void Load()
    {
        string name = gameManager.GetComponent<GameManagerMainMenu>().nameScene;
        SceneManager.LoadScene(name);
    }
}
