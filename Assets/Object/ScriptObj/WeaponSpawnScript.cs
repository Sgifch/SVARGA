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
    private Transform _effect;
    void Start()
    {
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnPosition;
        _spawnItem = Instantiate(spawnItem, spawnPosition, shiftSpawnRotation, gameObject.transform);
        _spawnItem.GetComponent<Animator>().SetTrigger("Spawn");
        _effect = gameObject.transform.GetChild(0);
    }

    public void TakeItem()
    {
        Destroy(_spawnItem);
        Destroy(_effect.gameObject);
        isEmpty = true;
    }
}
