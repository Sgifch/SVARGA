using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationStatManager : MonoBehaviour
{
    public int counterRoom;

    public void Awake()
    {
        LoadStatGeneration();
    }
    public void SaveStatGeneration()
    {
        PlayerPrefs.SetInt("counterRoom", counterRoom);
    }

    public void LoadStatGeneration()
    {
        if (PlayerPrefs.HasKey("counterRoom"))
        {
            counterRoom = PlayerPrefs.GetInt("counterRoom");
        }
    }

    public void DeleteStatGeneration()
    {
        PlayerPrefs.DeleteKey("counterRoom");
    }
}
