using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private Collider2D collision;
    private bool isStay;

    private void OnTriggerEnter2D(Collider2D  _collision)
    {
        collision = _collision;
        isStay = true;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        collision = null;
        isStay = false;
    }

    private void Update()
    {
        if (isStay)
        {
            if(collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindWithTag("GenerationManager").GetComponent<GenerationStatManager>().SaveStatGeneration();
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().RestartGeneration();
            }
        }
    }

}
