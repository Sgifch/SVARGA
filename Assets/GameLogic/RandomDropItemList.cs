using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropItemList : MonoBehaviour
{
    public List<GameObject> itemHealth = new List<GameObject>();
    public List<GameObject> itemUnHealth = new List<GameObject>();

    public List<float> dropChance = new List<float>();

    private void Start()
    {
        GameObject item = RandomDropItem();
        if (item != null)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }

    public GameObject RandomDropItem()
    {
        float r = Random.Range(0f, 1f);
        GameObject item = null;
        int i = 0;
        foreach (float chance in dropChance)
        {
            if (r < chance)
            {
                break;
            }
            i++;
        }
        switch (i)
        {
            case 0:
                item = null;
                break;

            case 1:
                item = itemHealth[Random.Range(0, itemHealth.Count)-1];
                break;

            case 2:
                item = itemUnHealth[Random.Range(0, itemUnHealth.Count - 1)];
                break;
        }
        return item;
    }
}
