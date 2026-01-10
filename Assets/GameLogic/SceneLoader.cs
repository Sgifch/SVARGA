using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public PlayerStatManager playerStat;
    public string sceneName;
    public bool useGeneration;

    //Анимация перехода
    public Animator animChanger;
    public GameObject sceneChangerCanvas;
    public int n;

    public void Awake()
    {
        playerStat = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStat.SaveStat();
            if (useGeneration)
            {
                GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().DeleteStatGeneration();
            }

            sceneChangerCanvas.SetActive(true);
            animChanger.SetInteger("isTrigger", n);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartScene()
    {
        sceneChangerCanvas.SetActive(false);
    }
}
