using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFreeze : MonoBehaviour
{
    public MagicBookItem profile;
    public Animator anim;
    public void Final()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<FreezeState>().ActiveFreeze();
            collision.gameObject.GetComponent<EnemyControllHealthPoint>().Damage(profile.damage);
            print("Freeze");
            //anim.SetTrigger("Final");
        }
    }
}
