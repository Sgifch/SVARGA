using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {food, weapon}
public class itemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int maximumAmaunt;
    public string itemDescription;
}
