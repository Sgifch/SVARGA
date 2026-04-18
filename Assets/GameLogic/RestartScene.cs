using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public GameObject canvasChange;
    public GameObject blackout;

    private Collider2D collision;
    public bool isStay;

    private void Start()
    {
        UIControll ui = GameObject.FindWithTag("UIControll").GetComponent<UIControll>();
        canvasChange = ui.canvasChange;
        blackout = ui.blackout;
    }

    private void OnTriggerEnter2D(Collider2D  _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            isStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            isStay = false;
        }
    }

    private void Update()
    {
        if (isStay)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                print("dhf");
                GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().SaveStatGeneration();
                canvasChange.SetActive(true);
                blackout.GetComponent<LobbyLoadScene>().sceneName = "GenerationScene";
                blackout.GetComponent<Animator>().SetTrigger("Blackout");
                //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RestartGeneration();
            }
        }
    }

}
