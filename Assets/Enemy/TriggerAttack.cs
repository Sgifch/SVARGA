using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    public EnemyScriptableObject enemyProfile;
    public float intervalAttack = 2f;

    private int attackPoint;
    private GameObject collider;
    private bool isCollision = false;

    private Coroutine coroutineAttack;
    private void Start()
    {
        attackPoint = enemyProfile.attackPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollision = true;
            collider = collision.gameObject;

            coroutineAttack = StartCoroutine(AttackPlayerOverTime(collider));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollision = false;
            //collider = null;

            StopCoroutine(coroutineAttack);
            coroutineAttack = null;
        }

    }

    IEnumerator AttackPlayerOverTime(GameObject player)
    {
        while (isCollision)
        {
            if (player.GetComponent<ControllHealthPoint>() != null)
            {
                player.GetComponent<ControllHealthPoint>().Damage(attackPoint);
            }

            yield return new WaitForSeconds(intervalAttack);
        }
    }
}
