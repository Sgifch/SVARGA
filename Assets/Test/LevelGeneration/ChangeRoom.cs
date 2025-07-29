using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeRoom : MonoBehaviour
{
    private GameObject player;
    public Direction direction;
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        None
    }

    public Vector3 shiftUp;
    public Vector3 shiftDown;
    public Vector3 shiftRight;
    public Vector3 shiftLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform position = collision.transform;

            switch (direction)
            {
                case Direction.Up:
                    collision.transform.position += shiftUp;
                    break;

                case Direction.Down:
                    collision.transform.position += shiftDown;
                    break;

                case Direction.Right:
                    collision.transform.position += shiftRight;
                    break;

                case Direction.Left:
                    collision.transform.position += shiftLeft;
                    break;

            }
        }
    }
}
