using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {food, sword}
public class itemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public GameObject itemObject;
    public Sprite icon;
    public string itemName;
    public int maximumAmaunt;
    public string itemDescription;

}

