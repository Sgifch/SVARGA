using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/New Card")]
public class CardsObject : ScriptableObject
{
    public itemScriptableObject cards;
    public GameObject iconFontainMenu;
    public string description;
}
