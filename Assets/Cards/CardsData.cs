using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsList", menuName = "Cards/New CardsList")]
public class CardsData : ScriptableObject
{
    public List<CardsObject> cardsList = new List<CardsObject>();
}
