using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;

    public GameObject spawnEffect;

    private bool isStart = false;
    void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isStart && collision.gameObject.tag == "Player")
        {
            TriggerEventStart();
        }
    }

    public void TriggerEventStart()
    {
        Instantiate(spawnEffect, wall1.transform.position, wall1.transform.rotation);
        wall1.SetActive(true);
        Instantiate(spawnEffect, wall2.transform.position, wall1.transform.rotation);
        wall2.SetActive(true);
        isStart = true;
    }

    public void TriggerEventEnd()
    {
        Instantiate(spawnEffect, wall1.transform.position, wall1.transform.rotation);
        wall1.SetActive(false);
        Instantiate(spawnEffect, wall2.transform.position, wall1.transform.rotation);
        wall2.SetActive(false);
    }

    
}
