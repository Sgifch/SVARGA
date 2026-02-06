using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesSpell : MonoBehaviour
{
    public Animator anim;
    public EnemyScriptableObject enemy;
    public float speed = 0.1f;
    private GameObject player;
    private Vector3 startPosition;
    private bool isDamage;

    public float lifeTime;
    private float time = 0;

    private void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isDamage)
            {
                collision.gameObject.GetComponent<ControllHealthPoint>().Damage(enemy.attackPoint);
                anim.SetTrigger("Final");
                isDamage = true;
            }

        }
    }

    public void Final()
    {
        Destroy(gameObject);
    }
}
