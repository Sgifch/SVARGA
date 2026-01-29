using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KikimoraTrigger : MonoBehaviour
{
    public bool isTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTrigger = true;

            GameObject mashroom = gameObject.transform.parent.gameObject;
            if (mashroom.GetComponent<AIKikimora>().state != AIKikimora.State.Attack)
            {
                mashroom.GetComponent<AIKikimora>().followObject = collision.gameObject;
                mashroom.GetComponent<AIKikimora>().state = AIKikimora.State.Roaming;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
