using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBonus : MonoBehaviour
{
    public float speedMultiplier = 1.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ControllMove playerMove = collision.gameObject.GetComponent<ControllMove>();
            playerMove.speed *= speedMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveBasic playerMove = collision.gameObject.GetComponent<MoveBasic>();
            playerMove.speed /= speedMultiplier;
        }
    }
}
