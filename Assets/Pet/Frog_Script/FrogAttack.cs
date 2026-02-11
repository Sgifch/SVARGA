using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
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

    public PetSO frog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControllHealthPoint>().Damage(frog.AttackPoint);
        }
    }
}
