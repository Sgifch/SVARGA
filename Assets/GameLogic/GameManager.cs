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
    public GameObject signPanel;

    public GameObject screenDeath;
    private int counterEnemy;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        signPanel.SetActive(false);
    }

    void Update()
    {
        if (stat.healthPoint <= 0)
        {
            Death();
        }
    }

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

    public void ActivateTrigger()
    {
        counterEnemy++;
        if (counterEnemy == 3)
        {
            triggerZone.GetComponent<TriggerEvent>().TriggerEventEnd();
        }
    }
}
