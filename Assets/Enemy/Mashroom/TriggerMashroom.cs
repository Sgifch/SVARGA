using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMashroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.gameObject.tag == "Player")
        {
            GameObject mashroom = gameObject.transform.parent.gameObject;
            mashroom.GetComponent<AIMashroom>().followObject = collision.gameObject;
            mashroom.GetComponent<AIMashroom>().state = AIMashroom.State.Roaming;
        }
    }
}
