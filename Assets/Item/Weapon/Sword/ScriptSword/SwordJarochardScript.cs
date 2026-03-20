using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordJarochardScript : MonoBehaviour
{
    public Item item;
    private int _damage;
    private int _strong;
    private int sumDamage;
    private GameObject _effect;

    public int price;
    public int effectDamage;
    public float speed = 1.5f;
    public GameObject spawn;

    private void Start()
    {
        itemScriptableObject _Item = gameObject.GetComponent<Item>().item;
        swordItem _sworditem = (swordItem)_Item;
        _damage = _sworditem.damage;
        _effect = _sworditem.effect;
        _strong = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>().currentStrong;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyControllHealthPoint enemyProfile = collision.gameObject.GetComponent<EnemyControllHealthPoint>();
            sumDamage = _damage + _strong;
            GameObject.FindWithTag("UIControll").GetComponent<UIControll>().DamageUI(sumDamage);
            enemyProfile.Damage(sumDamage);
        }
    }

    public void SpawnMagic()
    {
        if (price <= GameObject.FindWithTag("Player").GetComponent<ControllManaPoint>().currentMana)
        {
            GameObject.FindWithTag("Player").GetComponent<ControllManaPoint>().SubstractManaPoint(price);
            Vector2 axesTransform = new Vector2(0, 0);
            Quaternion axesRotate = Quaternion.Euler(0, 0, 0);

            string axes = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllMove>().LastAxes();
            switch (axes)
            {
                case "u":
                    axesTransform = new Vector2(0, 1 * speed);
                    axesRotate = Quaternion.Euler(180, 0, 0);
                    break;

                case "d":
                    axesTransform = new Vector2(0, -1 * speed);
                    axesRotate = Quaternion.Euler(0, 0, 0);
                    break;

                case "l":
                    axesTransform = new Vector2(-1 * speed, 0);
                    axesRotate = Quaternion.Euler(0, 0, 270);
                    break;

                case "r":
                    axesTransform = new Vector2(1 * speed, 0);
                    axesRotate = Quaternion.Euler(0, 0, 90);
                    break;
            }

            GameObject wave = Instantiate(_effect, spawn.transform.position, axesRotate);
            wave.GetComponent<Rigidbody2D>().velocity = axesTransform;
        }
    }
}
