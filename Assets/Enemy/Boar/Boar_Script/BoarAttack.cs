using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAttack : MonoBehaviour
{
    private BoxCollider2D Collider2D;

    void Awake()
    {
        Collider2D = GetComponent<BoxCollider2D>();
    }

    public void BoxColliderTurnOn()
    {
        Collider2D.enabled = true;
    }

    public void BoxColliderTurnOff()
    {
        Collider2D.enabled = false;
    }

    public EnemyScriptableObject enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControllHealthPoint>().Damage(enemy.attackPoint);
        }
    }
}
