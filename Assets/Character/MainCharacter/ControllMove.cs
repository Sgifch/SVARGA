using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMove : MonoBehaviour
{
    //Движения и физика
    public Animator anim;
    public float speed;
    public Rigidbody2D rb;

    private float ForwardBehind;
    private float LeftRight;

    private Vector2 moveD;
    private Vector2 LastmoveD;

    public float shift = 1f;

    public bool isMove = false;
    public bool isAttack = false;
    public bool isShiftAttack = false;

    //Доступ к статам
    public inventoryManager dataItem;
    public GameObject uiControll;
    public ControllHealthPoint controllHP;
    public ControllManaPoint controllMana;

    //Магия
    //public GameObject spawnPointMagic;



    void Start()
    {
        controllHP = gameObject.GetComponent<ControllHealthPoint>();
        controllMana = gameObject.GetComponent<ControllManaPoint>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        dataItem = gameObject.GetComponent<inventoryManager>();
        uiControll = GameObject.FindWithTag("UIControll");

        anim.SetFloat("LastmoveDx", 0);
        anim.SetFloat("LastmoveDy", -1);
        LastmoveD = new Vector2(0, -1);
    }

    void Update()
    {
        if (!uiControll.GetComponent<UIControll>().isStay && !isAttack)
        {
            processInputs();
            Animated();
        }
        

        if (Input.GetMouseButtonDown(0) && !uiControll.GetComponent<UIControll>().isStay && !isAttack && !isMove)
        {
            AttackWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !uiControll.GetComponent<UIControll>().isStay && !isAttack && !isMove)
        {
            SpawnMagic();
        }

    }

    void FixedUpdate()
    {
        if (!isAttack)
        {
            Move();
        }
        

        isMove = rb.velocity.magnitude > 0.01f;

        if (isShiftAttack) //Сдвиг при атаке (мб потом более оптимизировано сделать) Сделать под состояния???
        {
            string _lastAxes = LastAxes();

            switch (_lastAxes)
            {
                case "u":
                    rb.velocity = new Vector2(0f, shift);
                    break;

                case "d":
                    rb.velocity = new Vector2(0f, -shift);
                    break;

                case "l":
                    rb.velocity = new Vector2(-shift, 0f);
                    break;

                case "r":
                    rb.velocity = new Vector2(shift, 0f);
                    break;
            }

            isShiftAttack = false;

        }
    }

    void processInputs()
    {

        ForwardBehind = Input.GetAxisRaw("Vertical");
        LeftRight = Input.GetAxisRaw("Horizontal");

        if ((ForwardBehind == 0 && LeftRight == 0) && (moveD.x != 0 || moveD.y != 0))
        {
            LastmoveD = moveD;
        }

        Vector2 rawInput = new Vector2(LeftRight, ForwardBehind);
        
        if(rawInput.magnitude > 1)
        {
            moveD = rawInput.normalized;
        }
        else
        {
            moveD = rawInput;
        }

        if (moveD != new Vector2(0, 0))
        {   
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
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
        else
        {
            return LastmoveD.y > 0 ? "u" : "d";
        }

            return lastAxes;
    }

    //Блок аттаки----------------------------------------------------------------
    void AttackWeapon()
    {
        if (!dataItem.slotsWeapon[0].isEmpty)
        {
            isAttack = true;
            isShiftAttack = true;
            inventorySlot weapon = dataItem.slotsWeapon[0];
            Vector3 spawnPosition = gameObject.transform.position;
            Vector2 vector = new Vector2(0, 1);

            Vector3 shift = new Vector3(0f, -0.35f, 0f); //Будет изменяться в зависимости от положения
            string _lastAxes = LastAxes();

            switch (_lastAxes)
            {
                case "u":
                    shift = new Vector3(0f, 0.1f, 0f);
                    vector = new Vector2(0f, 1f);
                    break;

                case "d":
                    shift = new Vector3(0f, -0.35f, 0f);
                    vector = new Vector2(0f, -1f);
                    break;

                case "l":
                    shift = new Vector3(-0.15f, -0.35f, 0f);
                    vector = new Vector2(-1f, 0f);
                    break;

                case "r":
                    shift = new Vector3(0.15f, -0.35f, 0f);
                    vector = new Vector2(1f, 0f);
                    break;
            }
            print(shift);

            
            

            GameObject attackWeapon = Instantiate(weapon.item.itemObject, spawnPosition + shift, gameObject.transform.rotation, gameObject.transform);

            //Если у оружия есть звук
            if (attackWeapon.GetComponent<AudioSource>() != null)
            {
                attackWeapon.GetComponent<AudioSource>().Play();
            }

            Animator animWeapon = attackWeapon.GetComponent<Animator>();


            animWeapon.SetFloat("LastMoveDx", vector.x);
            animWeapon.SetFloat("LastMoveDy", vector.y);
            anim.SetFloat("LastmoveDx", vector.x);
            anim.SetFloat("LastmoveDy", vector.y);
            anim.SetTrigger("Attack");
            animWeapon.SetTrigger("Attack");
        }
    }

    //Блок магии-------------------------------------------------------------------------------------
     public void SpawnMagic()
     {
        //Просмотр ячейки инвентаря
        if (!dataItem.slotsWeapon[1].isEmpty)
        {
            //Просмотр стоимости заклинания
            MagicBookItem magic = (MagicBookItem)dataItem.slotsWeapon[1].GetComponent<inventorySlot>().item;
            if (controllMana.currentMana > magic.price)
            {
                controllMana.SubstractManaPoint(magic.price);
                Instantiate(magic.magic, transform.position, transform.rotation);
            }
            
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
}
