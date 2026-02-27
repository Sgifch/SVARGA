using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPoisonousPuddle : MonoBehaviour
{
    public MagicBookItem profile;
    public Animator anim;
    
    public float lifeTime;
    public float interval;

    private float time = 0f;

    private Coroutine coroutineAttack;
    public List<Collider2D> enemyList = new List<Collider2D>();

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            anim.SetTrigger("Final");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyList.Add(collision);

            if(coroutineAttack == null)
            {
                coroutineAttack = StartCoroutine(AttackEnemyOverTime());
            }
            //coroutineAttack = StartCoroutine(AttackPlayerOverTime(collider));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyList.Remove(collision);
        }

    }

    public void ActivatedCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void Final()
    {
        //StopCoroutine(coroutineAttack);
        Destroy(gameObject);
    }

    //Урон всем кто находится в зоне
    IEnumerator AttackEnemyOverTime()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                //Копия списка для избежания ошибки
                List<Collider2D> enemyToDamage = new List<Collider2D>(enemyList);

                foreach (Collider2D collision in enemyToDamage)
                {
                    if (collision != null)
                    {
                        EnemyControllHealthPoint hpEnemy = collision.gameObject.GetComponent<EnemyControllHealthPoint>();
                        if (hpEnemy != null)
                        {
                            hpEnemy.Damage(profile.damage);
                        }
                    }
                }

                yield return new WaitForSeconds(interval);
            }
            else
            {
                coroutineAttack = null;
                yield break;
            }
        }
    }

}
