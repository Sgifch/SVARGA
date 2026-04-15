using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLoadTrigger : MonoBehaviour
{
    public string sceneName;
    public GameObject changeCanvas;
    public GameObject blackout;
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            changeCanvas.SetActive(true);
            blackout.GetComponent<LobbyLoadScene>().sceneName = sceneName;
            anim.SetTrigger("Blackout");
        }
    }
}
