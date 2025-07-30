using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rooms", menuName = "Rooms/New DataRooms")]
public class RoomData : ScriptableObject
{
    [Header("������� �������")]

    public List <GameObject> normalRoomUp = new List<GameObject>();
    public List<GameObject> normalRoomDown = new List<GameObject>();
    public List<GameObject> normalRoomRight = new List<GameObject>();

    [Header("������� � ��������")]

    public List<GameObject> treasureRoomUp = new List<GameObject>();
    public List<GameObject> treasureRoomDown = new List<GameObject>();
    public List<GameObject> treasureRoomRight = new List<GameObject>();

}
