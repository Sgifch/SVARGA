using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {foodItem, weapon}
public class itemScriptableObject : ScriptableObject
{
    public string itemName;
    public int maximumAmaunt;
    public string itemDescription;
}
