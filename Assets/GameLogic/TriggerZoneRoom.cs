using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneRoom : MonoBehaviour
{
    public GameObject roomManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            roomManager.GetComponent<RoomManager>().WallSpawn();
            Destroy(gameObject);
        }
    }

}
