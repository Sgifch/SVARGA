using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliisionMashroom : MonoBehaviour
{
    public GameObject effectLight;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (gameObject.GetComponent<AIMashroom>().state != AIMashroom.State.Attack)
            {
                Instantiate(effectLight, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                gameObject.GetComponent<AIMashroom>().state = AIMashroom.State.Attack;
            }

        }
    }

}
