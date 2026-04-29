using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionalGeneration : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();

    public GameObject postRestart;

    public Button buttonSave;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "GenerationScene")
        {
            buttonSave.interactable = false;
        }
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
