using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFrog : MonoBehaviour
{
    public bool isTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isTrigger = true;

            GameObject frog = gameObject.transform.parent.gameObject;
            AIFrog aiFrog = frog.GetComponent<AIFrog>();

            if (frog.GetComponent<AIFrog>().state != AIFrog.State.Attack)
            {
                frog.GetComponent<AIFrog>().currentEnemy = collision.gameObject.transform;
                frog.GetComponent<AIFrog>().state = AIFrog.State.Roaming;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;

        GameObject frog = gameObject.transform.parent.gameObject;
        AIFrog aiFrog = frog.GetComponent<AIFrog>();

        if (frog.GetComponent<AIFrog>().currentEnemy == collision.transform)
        {
            // Очищаем текущего врага
            aiFrog.currentEnemy = null;

            // Возвращаем в состояние Idle
            aiFrog.state = AIFrog.State.Idle;
        }
    }
}
