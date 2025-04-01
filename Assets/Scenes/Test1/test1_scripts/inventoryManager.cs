using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform inventoryPanel;
    public bool isOpened;

    public List<inventorySlot> slots = new List<inventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<inventorySlot>() != null) //проверка компонента
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<inventorySlot>()); //добавление в лист
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // DS: потом переставить на всё меню
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanel.SetActive(true);
            }
            else
            {
                UIPanel.SetActive(false);
            }
        }
    }
}
