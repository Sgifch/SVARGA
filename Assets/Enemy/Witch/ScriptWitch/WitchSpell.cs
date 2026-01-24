using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpell : MonoBehaviour
{
    public Animator anim;
    public EnemyScriptableObject enemy;

    public float lifeTime;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControllHealthPoint>().Damage(enemy.attackPoint);
            anim.SetTrigger("Final");
        }
    }

    public void Final()
    {
        Destroy(gameObject);
    }
}
