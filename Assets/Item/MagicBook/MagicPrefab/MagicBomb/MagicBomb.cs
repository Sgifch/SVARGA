using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBomb : MonoBehaviour
{
    public float timeToExplotion;
    public MagicBookItem profile;

    public Animator anim;
    private float time = 0f;
    private bool isExplotion;

    private void Update()
    {
        if (!isExplotion)
        {
            time += Time.deltaTime;
            if (time >= timeToExplotion)
            {
                anim.SetTrigger("Attack");
                isExplotion = true;
            }
        }

    }

    public void Finall()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControllHealthPoint>().Damage(profile.damage);
        }
    }

}
