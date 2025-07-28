using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
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

    private RoomVariants variants;
    private ManagerLevelGeneration managerGeneration;
    private int rand;
    public bool isSpawn = false;
    private float waitTime = 3f;

    private int counterRight = 0;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        managerGeneration = GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<ManagerLevelGeneration>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        if (!isSpawn)
        {
            switch (direction)
            {
                case Direction.Up:
                    break;

                case Direction.Down:
                    break;

                case Direction.Right:

                    if (managerGeneration.counterRight<managerGeneration.maxRight) 
                    {
                        rand = Random.Range(0, variants.rightRoom.Length);
                        Instantiate(variants.rightRoom[rand], transform.position, transform.rotation);
                        managerGeneration.counterRight++;
                    }
                    else
                    {
                        //Спавн затычки
                    }

                        break;

                case Direction.Left:
                    break;
            }

            isSpawn = true;
        }
    }
}
