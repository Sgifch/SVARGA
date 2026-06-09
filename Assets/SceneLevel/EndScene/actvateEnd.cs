using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actvateEnd : MonoBehaviour
{
    public GameObject endMenu;
    public string sceneName;
    public GameObject changeCanvas;
    public GameObject blackout;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        endMenu.SetActive(true);

        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllMove>().enabled = false;
        GameObject.FindGameObjectWithTag("UIControll").GetComponent<UIControll>().enabled = false;
    }

    public void Load()
    {
        changeCanvas.SetActive(true);
        blackout.GetComponent<LobbyLoadScene>().sceneName = sceneName;
        anim.SetTrigger("Blackout");
        
    }
}
