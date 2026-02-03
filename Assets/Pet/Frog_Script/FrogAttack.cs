using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void BoxColliderTurnOn()
    {
        boxCollider2D.enabled = true;
    }

    public void BoxColliderTurnOff()
    {
        boxCollider2D.enabled = false;
    }
}
