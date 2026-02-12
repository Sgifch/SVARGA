using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBookItem", menuName = "Inventory/Items/New MagicBook")]
public class MagicBookItem : itemScriptableObject
{
    public GameObject magic;
    public int damage;
    public int price;
    private void Start()
    {
        itemType = ItemType.magicBook;
    }
}
