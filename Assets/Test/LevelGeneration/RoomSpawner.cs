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

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        managerGeneration = GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<ManagerLevelGeneration>();
        //Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.5f);
        
    }

    public void Spawn()
    {

        if (!isSpawn)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (managerGeneration.counterUp < managerGeneration.maxUp)
                    {
                        rand = Random.Range(0, variants.upRoom.Length);
                        Instantiate(variants.upRoom[rand], transform.position, transform.rotation);
                        managerGeneration.counterUp++;
                    }
                    else
                    {
                        Instantiate(variants.endUpRoom, transform.position, transform.rotation);
                    }
                    break;

                case Direction.Down:

                    if (managerGeneration.counterDown < managerGeneration.maxDown)
                    {
                        rand = Random.Range(0, variants.downRoom.Length);
                        Instantiate(variants.downRoom[rand], transform.position, transform.rotation);
                        managerGeneration.counterDown++;
                    }
                    else
                    {
                        Instantiate(variants.endDownRoom, transform.position, transform.rotation);
                    }
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
                        Instantiate(variants.endRightRoom, transform.position, transform.rotation);
                    }
                        break;

                case Direction.Left:
                    break;
            }

            isSpawn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RoomPoint" && collision.GetComponent<RoomSpawner>().isSpawn)
        {
            print("deletRoom");
            Destroy(gameObject);
        }
    }
}
