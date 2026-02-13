using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MagicArrowScript : MonoBehaviour
{
    public MagicBookItem profile;
    public float lifeTime;
    public float speed;

    public Animator anim;

    void Start()
    {
        Running();
    }

    private void Running()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        string axes = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllMove>().LastAxes();
        switch (axes)
        {
            case "u":
                rb.velocity = new Vector2(0, 1 * speed);
                break;

            case "d":
                rb.velocity = new Vector2(0, -1 * speed);
                break;

            case "l":
                rb.velocity = new Vector2(-1 * speed, 0);
                break;

            case "r":
                rb.velocity = new Vector2(1 * speed, 0);
                break;
        }
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
