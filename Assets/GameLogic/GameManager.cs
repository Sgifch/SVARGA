using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public characterStat stat;
    public Transform spawnPoint;
    public Transform player;

    public GameObject screenDeath;
    void Start()
    {
        
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
        Time.timeScale = 1;
        screenDeath.SetActive(false);
        player.position = spawnPoint.position;
        stat.healthPoint = stat.maxHealthPoint;
    }
}
