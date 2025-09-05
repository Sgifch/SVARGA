using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomItem : MonoBehaviour
{
    void Start()
    {
        GameObject spawnItem = gameObject.GetComponent<RandomDrop>().RandomDropItem();
        Instantiate(spawnItem, transform.position, transform.rotation);
    }
}
