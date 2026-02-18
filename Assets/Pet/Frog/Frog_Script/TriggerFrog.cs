using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFrog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject frog = gameObject.transform.parent.gameObject;
            AIFrog aiFrog = frog.GetComponent<AIFrog>();

            if (frog.GetComponent<AIFrog>().state != AIFrog.State.Attack)
            {
                frog.GetComponent<AIFrog>().currentEnemy = collision.gameObject.transform;
                frog.GetComponent<AIFrog>().state = AIFrog.State.Roaming;
            }

        }
    }
}
