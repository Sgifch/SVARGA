using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject boar = gameObject.transform.parent.gameObject;
            if (boar.GetComponent<BoarAI>().playerTransform == null)
            {
                boar.GetComponent<BoarAI>().playerTransform = collision.transform;
                boar.GetComponent<BoarAI>().state = BoarAI.State.Roaming;
            }

        }
    }
}
