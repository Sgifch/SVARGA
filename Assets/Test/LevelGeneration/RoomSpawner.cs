using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private bool isProcessing = false;
    public Direction direction;
    public GameObject wall;
    private float shiftWall = 35;
    private OptionalGeneration optional;
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
        optional = GameObject.FindWithTag("Optional").GetComponent<OptionalGeneration>();
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        managerGeneration = GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<ManagerLevelGeneration>();
        //Destroy(gameObject, waitTime);
        float addRand = Random.Range(0f, 1f);
        //Invoke("Spawn", 0.5f + addRand);
        //Destroy(gameObject, waitTime);

        Spawn();
        
    }

    public void Spawn()
    {
        if (isProcessing) return;
        isProcessing = true;

        //ПРОВЕРКА: нужно ли вообще спавнить, если уже кто-то занял место
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (var col in colliders)
        {
            if (col.CompareTag("RoomPoint") && col != GetComponent<Collider2D>())
            {
                //Место уже занято
                wallSpawn();
                Destroy(gameObject);
                return;
            }
        }

        if (!isSpawn)
        {
            if (!IsPositionValid())
            {
                wallSpawn();
                Destroy(gameObject);
                return;
            }

            switch (direction)
            {
                case Direction.Up:
                    if (managerGeneration.counterUp < managerGeneration.maxUp)
                    {
                        rand = Random.Range(0, variants.upRoom.Count);
                        optional.rooms.Add(Instantiate(variants.upRoom[rand], transform.position, transform.rotation));
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
                        optional.rooms.Add(Instantiate(variants.downRoom[rand], transform.position, transform.rotation));
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
                        optional.rooms.Add(Instantiate(variants.rightRoom[rand], transform.position, transform.rotation));
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

        Destroy(gameObject, 0.1f);
    }

    private bool IsPositionValid()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Room") || (hit.CompareTag("RoomPoint") && hit != GetComponent<Collider2D>()))
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isProcessing) return; // Не обрабатывать во время спавна

        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().isSpawn)
        {
            wallSpawn();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Room"))
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
                Instantiate(wall, transform.position - new Vector3(0f, shiftWall, 0f), transform.rotation); //Потом rotation переделать под стены
                break;

            case Direction.Down:
                Instantiate(wall, transform.position + new Vector3(0f, shiftWall, 0f), transform.rotation);
                break;

            case Direction.Right:
                Instantiate(wall, transform.position - new Vector3(shiftWall, 0f, 0f), transform.rotation);
                break;
        }
    }
}


    
