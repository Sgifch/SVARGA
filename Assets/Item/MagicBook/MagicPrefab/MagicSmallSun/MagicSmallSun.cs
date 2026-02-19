using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSmallSun : MonoBehaviour
{
    public MagicBookItem profile;
    public float speed;
    public Animator anim;
    public GameObject target;
    public float lifeTime;

    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }
        
        if (target != null)
        {
            Move();
        }
    
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
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
            Destroy(gameObject);
            //anim.SetTrigger("Final");
        }
    }
}
