using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSign : MonoBehaviour
{
    public string text;
    public GameObject signPanel;
    public GameObject uiControll;

    private void Start()
    {
        uiControll = GameObject.FindWithTag("UIControll");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //SignPanelShow(true);
            uiControll.GetComponent<UIControll>().SignOpen(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //SignPanelShow(false);
            uiControll.GetComponent<UIControll>().SignClose();
        }
    }

    /*public void SignPanelShow(bool active)
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
    }*/
}
