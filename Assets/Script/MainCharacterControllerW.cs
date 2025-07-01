using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCharacterControllerW : MonoBehaviour
{
    //Косметика
    public Sprite spriteInGrass; // потом убрать
    private Material mainCharacterMaterial;

    //Движения и физика
    public Animator anim;

    public float speed;

    public Rigidbody2D rb;

    float ForwardBehind;
    float LeftRight;

    private Vector2 moveD;
    private Vector2 LastmoveD;

    public Transform axesPlayer;

    //Доступ к статам
    public characterStat stat;
    public inventoryManager dataItem;

    public float changeHealthPoint;
    public float changeMannaPoint;

    public Image imageHealthBar;
    public Image imageMannaBar;

    //Подсказки
    public GameObject signPanel;

    //Реакция на касание врага
    public bool enemyStaticTouch;
    public bool takeDamage = false;
    public float _timeDamage = 1f;
    private float timeDamage = 0f;

    //Время
    private float time = 0f;

    //Коллайдеры
    Collision2D collider = null;
    Collider2D collider_tr = null;
    void Start()
    {
        enemyStaticTouch = false;
    
        changeHealthPoint = (float)(stat.healthPoint) / (stat.maxHealthPoint);
        changeMannaPoint = (float)(stat.mannaPoint) / (stat.maxMannaPoint);

        imageHealthBar.fillAmount = changeHealthPoint;
        imageMannaBar.fillAmount = changeMannaPoint;

        mainCharacterMaterial = GetComponent<SpriteRenderer>().material;

    }

    void Update()
    {
        time += Time.deltaTime;
        timeDamage += Time.deltaTime;

        processInputs();
        //Move();
        Animated();

        if (Input.GetMouseButtonDown(0))
        {
            print("Атака1");
            AttackWeapon();
        }

        if (enemyStaticTouch)
        {
            AttackEnemyStatic();
        }

        if (timeDamage >= _timeDamage)
        {
            takeDamage = false;
            Damage();
            timeDamage = 0f;
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    void processInputs()
    {
        ForwardBehind = Input.GetAxisRaw("Vertical");
        LeftRight = Input.GetAxisRaw("Horizontal");

        if ((ForwardBehind == 0 && LeftRight == 0) && moveD.x != 0 || moveD.y != 0) 
        {
            LastmoveD = moveD;
        }

        moveD = new Vector2(LeftRight, ForwardBehind).normalized;
    }

    //Столкновения
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyStaticTouch = true;
        collider = collision;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyStaticTouch = false;
        collider = collision;
    }

    //Триггер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "grass")
        {
            ChangeSprite(true);
        }*/

        switch (collision.gameObject.tag)
        {
            case "enemyStatic":
                enemyStaticTouch = true;
                collider_tr = collision;
                break;

            case "sign":
                collider_tr = collision;
                SignShow(true);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enemyStatic":
                enemyStaticTouch = false;
                collider_tr = null;
                break;

            case "sign":
                SignShow(false);
                collider_tr = null;
                break;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveD.x * speed, moveD.y * speed); 
    }

    //Блок получения урона --------------------------------------------------------------------
    void AttackEnemyStatic() //Урон от недвижимых врагов 
    {
        enemyProfile attackPoint = collider_tr.gameObject.GetComponent<enemy>().enemyStat;  //Эту часть переделать для разных врагов а то фигня
        float intervalTime = collider_tr.gameObject.GetComponent<enemy>().intervalAttack;

        takeDamage = true;

        Damage();

        if (time >= intervalTime)
        {
            stat.healthPoint = stat.healthPoint - attackPoint.attack;

            changeHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
            imageHealthBar.fillAmount = changeHealthPoint;
            time = 0;

            print("Attack");
        }
        
    }

    //Блок нанесения урона----------------------------------------------------------------
    void AttackWeapon()
    {
        if (dataItem.slotsWeapon[0] != null)
        {

            inventorySlot weapon = dataItem.slotsWeapon[0];
            Vector3 spawnPosition = gameObject.transform.position;
            Vector3 displacement = new Vector3(-0.29f, -0.3f, 0f); //Будет изменяться в зависимости от положения
            GameObject attackWeapon = Instantiate(weapon.item.itemObject, spawnPosition + displacement, gameObject.transform.rotation, gameObject.transform);
            Animator animWeapon = attackWeapon.GetComponent<Animator>();

            animWeapon.SetFloat("LastMoveDx", LastmoveD.x);
            animWeapon.SetFloat("LastMoveDy", LastmoveD.y);
            animWeapon.SetTrigger("Attack");
        }
    }

    //Блок использования вещей инвентаря -------------------------------------------------
    public void UseFood(inventorySlot useItem)
    {
        foodItem food = (foodItem)useItem.item; //Явное преобразование к классу
        int recoveryHealth = food.healthAmount;
        stat.healthPoint = stat.healthPoint + recoveryHealth;

        ChangeHealth();
    }

    public void ChangeHealth()
    {
        changeHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
        imageHealthBar.fillAmount = changeHealthPoint;
    }

    public void SignShow(bool active)
    {
        if (active)
        {
            signPanel.SetActive(true);

            TMP_Text text_sign = signPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>(); //Мб это потом объявить в Start а то как то по тупому
            string _text = collider_tr.GetComponent<TextSign>().text;
            text_sign.text = _text;
        }
        else
        {
            signPanel.SetActive(false);
        }
    }

    //Блок апимации----------------------------------------------------------------------------------
    void Animated()
    {
        anim.SetFloat("RouteFB", moveD.y);
        anim.SetFloat("RouteLR", moveD.x);
        anim.SetFloat("moveD", moveD.magnitude);
        anim.SetFloat("LastmoveDx", LastmoveD.x);
        anim.SetFloat("LastmoveDy", LastmoveD.y);
    }

    void Damage()
    {
        mainCharacterMaterial.SetFloat("_takeDamage", takeDamage ? 1f : 0f);
    }
    public void ChangeSprite(bool change)
    {
        if (change)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteInGrass;
        }
    }
}
