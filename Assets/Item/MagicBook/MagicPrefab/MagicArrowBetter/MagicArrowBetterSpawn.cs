using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowBetterSpawn : MonoBehaviour
{
    public GameObject magic;
    public float speed;
    void Start()
    {
        GameObject arrow1 = Instantiate(magic, transform.position, Quaternion.Euler(0, 0, 0));
        GameObject arrow2 = Instantiate(magic, transform.position, Quaternion.Euler(180, 0, 0));
        GameObject arrow3 = Instantiate(magic, transform.position, Quaternion.Euler(0, 0, 90));
        GameObject arrow4 = Instantiate(magic, transform.position, Quaternion.Euler(0, 0, 270));

        arrow1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 * speed);
        arrow2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * speed);
        arrow3.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed, 0);
        arrow4.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * speed, 0);

        Destroy(gameObject);

    }
}
