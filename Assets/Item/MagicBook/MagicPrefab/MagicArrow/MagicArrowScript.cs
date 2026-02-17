using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicArrowScript : MonoBehaviour
{
    public MagicBookItem profile;
    public float lifeTime;
    public float speed;

    public Animator anim;
    private float time = 0f;

    void Start()
    {
        Running();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }
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
                transform.Rotate(180, 0, 0);
                rb.velocity = new Vector2(0, -1 * speed);
                break;

            case "l":
                transform.Rotate(0, 0, 90);
                rb.velocity = new Vector2(-1 * speed, 0);
                break;

            case "r":
                transform.Rotate(0, 0, 270);
                rb.velocity = new Vector2(1 * speed, 0);
                break;
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
