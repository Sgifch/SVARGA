using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> wall = new List<GameObject>();
    public List<GameObject> triggerZone = new List<GameObject>();
    public EnemyList enemy;
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
        for (int i = 0; i<currentAmountEnemy; i++)
        {
            EnemySpawn();
        }
    }

    
    void Update()
    {
        if (currentEnemy.transform.childCount == 0)
        {
            //Что происходит после победы
            //Подсчёт пройденных комнат
            GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().counterRoom++;
            WallDestroy();
            Destroy(gameObject);
        }

    }

    public void EnemySpawn()
    {
        int shiftPositionX;
        int shiftPositionY;

        shiftPositionX = Random.Range(-maxShiftPosition, maxShiftPosition);
        shiftPositionY = Random.Range(-maxShiftPosition, maxShiftPosition);

        int rndEnemy = Random.Range(0, enemy.enemyList.Count);
        print(rndEnemy);
        Instantiate(enemy.enemyList[rndEnemy], transform.position + new Vector3(shiftPositionX, shiftPositionY, 0), transform.rotation, currentEnemy.transform);
    }

    public void WallSpawn()
    {
        foreach (GameObject _wall in wall)
        {
            Instantiate(spawnEffect, _wall.transform.position, _wall.transform.rotation);
            _wall.SetActive(true);
        }
    }

    public void WallDestroy()
    {
        foreach (GameObject _wall in wall)
        {
            Instantiate(spawnEffect, _wall.transform.position, _wall.transform.rotation);
            Destroy(_wall);
        }
    }
}
