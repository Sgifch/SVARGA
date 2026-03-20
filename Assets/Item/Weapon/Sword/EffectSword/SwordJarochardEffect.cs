using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordJarochardEffect : MonoBehaviour
{
    public int damage;

    public void Final()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControllHealthPoint>().Damage(damage);

            Final();
            //anim.SetTrigger("Final");
        }
    }
}
