using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontainFunction : MonoBehaviour
{
    public CardsData _cardsList;
    public bool isSpawn = false;
    public bool isTake = false;
    private GameObject fontainUI;
    public void FontainStart()
    {
        if (!isSpawn)
        {
            fontainUI = GameObject.FindWithTag("FontainUI");
            if (fontainUI.transform.GetChild(0).childCount > 0)
            {
                for (int i=0; i<fontainUI.transform.GetChild(0).childCount; i++)
                {
                    Destroy(fontainUI.transform.GetChild(0).GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                int r = Random.Range(0, _cardsList.cardsList.Count);
                Instantiate(_cardsList.cardsList[r].iconFontainMenu, fontainUI.transform.position, fontainUI.transform.rotation, fontainUI.transform.GetChild(0));
            }

            isSpawn = true;
        }

    }

    public void FontainEnd()
    {
        if (fontainUI.transform.GetChild(0).childCount == 0)
        {
            isTake = true;
        }
    }

}
