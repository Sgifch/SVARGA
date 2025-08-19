using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSign : MonoBehaviour
{
    public string _text;
    public GameObject signPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SignPanelShow(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SignPanelShow(false);
        }
    }

    public void SignPanelShow(bool active)
    {
        if (active)
        {
            signPanel.SetActive(true);

            TMP_Text text_sign = signPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>(); 
            text_sign.text = _text;
        }
        else
        {
            signPanel.SetActive(false);
        }
    }
}
