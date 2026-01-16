using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMashroom : MonoBehaviour
{
    public bool isTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.gameObject.tag == "Player")
        {
            isTrigger = true;

            GameObject mashroom = gameObject.transform.parent.gameObject;
            if (mashroom.GetComponent<AIMashroom>().state != AIMashroom.State.Attack)
            {
                mashroom.GetComponent<AIMashroom>().followObject = collision.gameObject;
                mashroom.GetComponent<AIMashroom>().state = AIMashroom.State.Roaming;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
