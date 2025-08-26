using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> wall = new List<GameObject>();
    public List<GameObject> triggerZone = new List<GameObject>();
    public List<GameObject> enemy = new List<GameObject>(); //Лист мобов
    //public List<GameObject> currentEnemy = new List<GameObject>();
    public GameObject currentEnemy;
    public GameObject spawnEffect;

    public int maxSpawn;
    public int minSpawn;
    //public int countDeath;
    public int currentAmountEnemy;

    public int maxShiftPosition;
    
    void Start()
    {
        currentAmountEnemy = Random.Range(minSpawn, maxSpawn);
        for (int i = 0; i<maxSpawn; i++)
        {
            EnemySpawn();
        }
    }

    
    void Update()
    {
        if (currentEnemy.transform.childCount == 0)
        {
            //Что происходит после победы
        }

    }

    public void EnemySpawn()
    {
        int shiftPositionX;
        int shiftPositionY;

        shiftPositionX = Random.Range(1, maxShiftPosition);
        shiftPositionY = Random.Range(1, maxShiftPosition);

        Instantiate(enemy[0], transform.position + new Vector3(shiftPositionX, shiftPositionY, 0), transform.rotation, currentEnemy.transform);
    }
}
