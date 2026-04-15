using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyLobby : MonoBehaviour
{
    public Animator anim;
    public bool isStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isStart)
            {
                anim.SetTrigger("End");
            }
            else
            {
                anim.SetTrigger("Start");
                isStart = true;
            }
        }
    }

    public void Died()
    {
        Destroy(gameObject);
    }
}
