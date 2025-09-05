using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WeaponSpawnScript : MonoBehaviour
{
    public GameObject spawnItem;
    public Vector3 shiftSpawnPosition;
    public Quaternion shiftSpawnRotation;
    public float speed;
    public Vector3 shiftAnim;

    public bool isEmpty = false; //Это потом должно ишрать свою роль при создании сохранений
                                //Для контроля уровня сделать потом ScriptableObject

    private GameObject _spawnItem;
    private Vector3 _itemPosition;
    private Transform _effect;
    void Start()
    {
        spawnItem = gameObject.GetComponent<RandomDrop>().RandomDropItem();
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnPosition;
        _spawnItem = Instantiate(spawnItem, spawnPosition, shiftSpawnRotation, gameObject.transform);
        //_spawnItem.GetComponent<Animator>().SetTrigger("Spawn");
        _effect = gameObject.transform.GetChild(0);
        _itemPosition = _spawnItem.transform.position;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        _spawnItem.transform.position = Vector3.Lerp(_itemPosition, _itemPosition+shiftAnim, t);
    }

    public void TakeItem()
    {
        Destroy(_spawnItem);
        Destroy(_effect.gameObject);
        isEmpty = true;
    }
}
