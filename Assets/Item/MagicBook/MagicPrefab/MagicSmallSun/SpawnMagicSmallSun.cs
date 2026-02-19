using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMagicSmallSun : MonoBehaviour
{
    public GameObject magic;
    public int count;
    public float radius = 10f;
    public LayerMask layer;

    private List<GameObject> nearbyEnemy = new List<GameObject>(); 
    private void Start()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                nearbyEnemy.Add(collider.gameObject);
            }
        }

        SpawnMagic();
    }

    private void SpawnMagic()
    {
        for (int i=0; i<count; i++)
        {
            GameObject currentMagic = Instantiate(magic, transform.position, transform.rotation);

            int n = Random.Range(0, nearbyEnemy.Count);
            currentMagic.GetComponent<MagicSmallSun>().target = nearbyEnemy[n];
        }

        Destroy(gameObject);
    }
}
