using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevelGeneration : MonoBehaviour
{
    [Header("Количество комнат")]
    public int maxUp;
    public int maxDown;
    public int maxLeft;
    public int maxRight;

    public int counterUp;
    public int counterDown;
    public int counterLeft;
    public int counterRight;

    [Header("Список комнат")]
    public RoomData roomData;

    [Header("Частота появления комнат")]
    public int fTreasureRoom = 1;
    public int fNormalRoom = 5;

    private RoomVariants roomList;
    private void Awake()
    {
        roomList = GameObject.Find("RoomsList").GetComponent<RoomVariants>();
        CreateListRoom();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGeneration()
    {

    }

    //Загрузка массива комнат
    private void CreateListRoom()
    {
        for(int i=0; i<fNormalRoom; i++)
        {
            foreach (GameObject room in roomData.normalRoomUp)
            {
                roomList.upRoom.Add(room);
            }

            foreach (GameObject room in roomData.normalRoomDown)
            {
                roomList.downRoom.Add(room);
            }

            foreach (GameObject room in roomData.normalRoomRight)
            {
                roomList.rightRoom.Add(room);
            }

        }

        for (int i = 0; i<fTreasureRoom; i++)
        {
            foreach (GameObject room in roomData.treasureRoomUp)
            {
                roomList.upRoom.Add(room);
            }

            foreach (GameObject room in roomData.treasureRoomDown)
            {
                roomList.downRoom.Add(room);
            }

            foreach (GameObject room in roomData.treasureRoomRight)
            {
                roomList.rightRoom.Add(room);
            }
        }
    }
}
