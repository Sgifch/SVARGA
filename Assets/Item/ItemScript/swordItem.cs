using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "swordItem", menuName = "Inventory/Items/New swordItem")] //�������� ������
public class swordItem : itemScriptableObject
{
    public int damage;
    public GameObject effect;
    private void Start()
    {
        itemType = ItemType.sword; //��� item
    }

}
