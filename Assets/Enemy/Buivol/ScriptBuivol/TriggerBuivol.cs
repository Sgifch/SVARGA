using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBuivol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject buivol = gameObject.transform.parent.gameObject;
            if (buivol.GetComponent<AIBuivol>().followObject == null )
            {
                buivol.GetComponent<AIBuivol>().followObject = collision.gameObject;
                buivol.GetComponent<AIBuivol>().state = AIBuivol.State.Roaming;
            }

        }
    }
}
