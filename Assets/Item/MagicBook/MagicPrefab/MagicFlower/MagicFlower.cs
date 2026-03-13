using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlower : MonoBehaviour
{
    public float lifeTime;
    public Animator anim;
    private float time = 0f;

    private Coroutine _Recovery;
    public float interval;

    private GameObject player;
    public int health;

    private void Start()
    {
        
    }
    void Update()
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

    public void ActiveArea()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        _Recovery = StartCoroutine(Recovery());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    private IEnumerator Recovery()
    {
        while (true)
        {
            if (player != null)
            {
                player.GetComponent<ControllHealthPoint>().Recovery(health);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
