using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public GameObject wall;
    public Vector3 shiftWall;
    public enum Direction
    {
        Up,
        Down,
        Right,
        None
    }

    private RoomVariants variants;
    private ManagerLevelGeneration managerGeneration;
    private int rand;
    public bool isSpawn = false;
    //public bool isColl
    private float waitTime = 2f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        managerGeneration = GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<ManagerLevelGeneration>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.5f);
        //Destroy(gameObject, waitTime);
        
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
                        rand = Random.Range(0, variants.upRoom.Count);
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
                        rand = Random.Range(0, variants.downRoom.Count);
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
                        rand = Random.Range(0, variants.rightRoom.Count);
                        Instantiate(variants.rightRoom[rand], transform.position, transform.rotation);
                        managerGeneration.counterRight++;
                    }
                    else
                    {
                        Instantiate(variants.endRightRoom, transform.position, transform.rotation);
                    }
                        break;
            }

            isSpawn = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().isSpawn)
        {

            print(transform.parent.gameObject + ": Delete Room");
            wallSpawn();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("RoomPoint"))
        {
            wallSpawn();
            Destroy(gameObject);
        }
    }

    private void wallSpawn()
    {
        switch (direction)
        {
            case Direction.Up:
                Instantiate(wall, transform.position + shiftWall, transform.rotation); //Потом rotation переделать под стены
                break;

            case Direction.Down:
                Instantiate(wall, transform.position + shiftWall, transform.rotation);
                break;

            case Direction.Right:
                Instantiate(wall, transform.position + shiftWall, transform.rotation);
                break;
        }
    }
}
