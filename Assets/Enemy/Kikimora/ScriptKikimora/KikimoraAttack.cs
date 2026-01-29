using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KikimoraAttack : MonoBehaviour
{
    public EnemyScriptableObject enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControllHealthPoint>().Damage(enemy.attackPoint);
            //kikimora.GetComponent<AIKikimora>().state = AIKikimora.State.Roaming;
        }
    }
}
