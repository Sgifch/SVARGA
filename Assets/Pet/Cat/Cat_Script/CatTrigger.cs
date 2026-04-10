using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject cat = gameObject.transform.parent.gameObject;
            CatAI aiCat = cat.GetComponent<CatAI>();

            if (cat.GetComponent<CatAI>().state != CatAI.State.Attack)
            {
                cat.GetComponent<CatAI>().currentEnemy = collision.gameObject.transform;
                cat.GetComponent<CatAI>().state = CatAI.State.Roaming;
            }

        }
    }
}
