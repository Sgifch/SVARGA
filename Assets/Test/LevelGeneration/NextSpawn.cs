using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSpawn : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        None
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Room")
        {
            print("Room");
            return;
        }
        print("Пусто");
        Destroy(gameObject);
    }
}
