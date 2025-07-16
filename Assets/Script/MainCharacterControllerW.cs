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

    public bool isMove = false;
    public bool isAttack = false;
    public bool isShiftAttack = false;

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

        if (Input.GetMouseButtonDown(0) && !dataItem.isOpened && !isAttack && !isMove)
        {
            
            AttackWeapon();
        }

        if (enemyStaticTouch)
        {
            //AttackEnemyStatic();
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

        isMove = rb.velocity.magnitude > 0.1f;

        if (isShiftAttack) //Сдвиг при атаке (мб потом более оптимизировано сделать)
        {
            string _lastAxes = LastAxes();

            switch (_lastAxes)
            {
                case "u":
                    rb.velocity = new Vector2(0f, 7f); 
                    break;

                case "d":
                    rb.velocity = new Vector2(0f, -7f);
                    break;

                case "l":
                    rb.velocity = new Vector2(-7f, 0f);
                    break;

                case "r":
                    rb.velocity = new Vector2(7f, 0f);
                    break;
            }

            isShiftAttack = false;

        }
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

    //Блок движения --------------------------------------------------------------------------
    void Move()
    {
        rb.velocity = new Vector2(moveD.x * speed, moveD.y * speed);
    }

    public string LastAxes()
    {
        string lastAxes = " ";

        if (Mathf.Approximately(LastmoveD.x, 0f) && Mathf.Approximately(LastmoveD.y, -1f))
        {
            // Действия для направления вниз
            lastAxes = "d";

        }
        else if (Mathf.Approximately(LastmoveD.x, 1f) && Mathf.Approximately(LastmoveD.y, 0f))
        {
            // Действия для направления вправо
            lastAxes = "r";
        }
        else if (Mathf.Approximately(LastmoveD.x, 0f) && Mathf.Approximately(LastmoveD.y, 1f))
        {
            // Действия для направления вверх
            lastAxes = "u";
        }
        else if (Mathf.Approximately(LastmoveD.x, -1f) && Mathf.Approximately(LastmoveD.y, 0f))
        {
            // Действия для направления влево
            lastAxes = "l";
        }

        return lastAxes;
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

        }
        
    }

    //Блок нанесения урона----------------------------------------------------------------
    void AttackWeapon()
    {
        if (!dataItem.slotsWeapon[0].isEmpty)
        {

            inventorySlot weapon = dataItem.slotsWeapon[0];
            Vector3 spawnPosition = gameObject.transform.position;

            Vector3 shift = new Vector3(0f, -0.35f, 0f); //Будет изменяться в зависимости от положения
            string _lastAxes = LastAxes();

            switch (_lastAxes)
            {
                case "u":
                    shift = new Vector3(0f, 0.1f, 0f);
                    break;

                case "d":
                    shift = new Vector3(0f, -0.35f, 0f);
                    break;

                case "l":
                    shift = new Vector3(-0.15f, -0.35f, 0f);
                    break;

                case "r":
                    shift = new Vector3(0.15f, -0.35f, 0f);
                    break;
            }

            isAttack = true;
            isShiftAttack = true;

            GameObject attackWeapon = Instantiate(weapon.item.itemObject, spawnPosition + shift, gameObject.transform.rotation, gameObject.transform);
            Animator animWeapon = attackWeapon.GetComponent<Animator>();
            

            animWeapon.SetFloat("LastMoveDx", LastmoveD.x);
            animWeapon.SetFloat("LastMoveDy", LastmoveD.y);
            anim.SetFloat("LastmoveDx", LastmoveD.x);
            anim.SetFloat("LastmoveDy", LastmoveD.y);
            anim.SetTrigger("Attack");
            animWeapon.SetTrigger("Attack");
        }
    }

    //Блок использования вещей инвентаря -------------------------------------------------
    public void UseFood(inventorySlot useItem) //засунуть потом в другой код
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
