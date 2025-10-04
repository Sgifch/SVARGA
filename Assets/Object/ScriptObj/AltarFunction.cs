using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarFunction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ExitGeneration();
        }

    }
}
