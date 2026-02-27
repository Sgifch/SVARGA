using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : MonoBehaviour
{
    public MagicBookItem profile;
    public Animator anim;

    public float lifeTime;
    public float interval;

    public float speed;
    public GameObject bullet;
    //public int count;

    private float time = 0f;

    private Coroutine coroutineAttack;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }
    }

    public void ActivateAttack()
    {
        coroutineAttack = StartCoroutine(AttackEnemyOverTime());
    }

    public void Final()
    {
        Destroy(gameObject);
    }

    IEnumerator AttackEnemyOverTime()
    {
        while (true)
        {
            //Какой-то колхоз
            //GameObject obj;
            //-->
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * speed, 0);

            GameObject obj1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 45));
            obj1.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1).normalized * speed;

            GameObject obj2 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -45));
            obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1).normalized * speed;

            GameObject obj3 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 135));
            obj3.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1).normalized * speed;

            GameObject obj4 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -135));
            obj4.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1).normalized * speed;

            GameObject obj5 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
            obj5.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed, 0);

            GameObject obj6 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
            obj6.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 * speed);

            GameObject obj7 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 270));
            obj7.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * speed);

            yield return new WaitForSeconds(interval);
        }
    }
}
