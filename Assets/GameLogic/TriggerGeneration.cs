using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGeneration : MonoBehaviour
{
    public GameObject sceneChangerCanvas;
    public Animator animChanger;
    public int n;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sceneChangerCanvas.SetActive(true);

        }

        sceneChangerCanvas.SetActive(true);
        animChanger.SetInteger("isTrigger", n);
    }

    public void sceneChange()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().LoadGeneration();
    }
}
