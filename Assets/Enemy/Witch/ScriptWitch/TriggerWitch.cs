using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject witch = gameObject.transform.parent.gameObject;
            if (witch.GetComponent<WitchAI>().state != WitchAI.State.Attack)
            {
                witch.GetComponent<WitchAI>().followObject = collision.gameObject;
                witch.GetComponent<WitchAI>().state = WitchAI.State.Roaming;
            }

        }
    }
}
