using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    public float lifeTime;

    public Animator anim;
    private float time = 0f;
    private void Start()
    {
        Transform parent = GameObject.FindWithTag("Player").GetComponent<Transform>();
        gameObject.transform.SetParent(parent);
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
}
