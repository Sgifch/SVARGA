using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WeaponSpawnScript : MonoBehaviour
{
    public GameObject spawnItem;
    public Vector3 shiftSpawnPosition;
    public Quaternion shiftSpawnRotation;

    public bool isEmpty = false; //Это потом должно ишрать свою роль при создании сохранений
                                //Для контроля уровня сделать потом ScriptableObject

    private GameObject _spawnItem;
    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnPosition;
        _spawnItem = Instantiate(spawnItem, spawnPosition, shiftSpawnRotation, gameObject.transform);
    }

    public void TakeItem()
    {
        Destroy(_spawnItem);
        isEmpty = true;
    }
}
