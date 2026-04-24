using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionalGeneration : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();

    public GameObject postRestart;

    private void Start()
    {
        Invoke("AddPostPrefab", 20);
    }

    private void AddPostPrefab()
    {
        if (GameObject.FindWithTag("EndAltar") == null && GameObject.FindWithTag("PostRestart") == null)
        {
            Instantiate(postRestart, rooms[rooms.Count-1].transform);
            print("isSpawn");
        }
    }
}
