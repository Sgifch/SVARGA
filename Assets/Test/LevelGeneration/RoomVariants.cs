using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public List<GameObject> upRoom = new List<GameObject>();
    public List<GameObject> downRoom = new List<GameObject>();
    public List<GameObject> rightRoom = new List<GameObject>();
    public List<GameObject> leftRoom = new List<GameObject>();

    public GameObject endUpRoom;
    public GameObject endDownRoom;
    public GameObject endRightRoom;
}
