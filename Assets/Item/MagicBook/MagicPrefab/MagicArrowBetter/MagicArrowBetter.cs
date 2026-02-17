using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowBetter : MonoBehaviour
{
    public MagicBookItem profile;
    public float lifeTime;
    public Animator anim;

    private float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }
    }
    public void Final()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControllHealthPoint>().Damage(profile.damage);
            anim.SetTrigger("Final");
        }
    }
}
