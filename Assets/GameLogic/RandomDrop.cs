using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public List<GameObject> item = new List<GameObject>();
    public List<float> dropChance = new List<float>();

    public GameObject RandomDropItem()
    {
        float r = Random.Range(0f, 1f);
        int i = 0;
        foreach (float chance in dropChance)
        {
            if (r < chance)
            {
                break;
            }
            i++;
        }
        return item[i];
    }
}
