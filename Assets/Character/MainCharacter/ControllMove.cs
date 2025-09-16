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

    public bool isMove = false;
    public bool isAttack = false;
    public bool isShiftAttack = false;

    //Доступ к статам
    public inventoryManager dataItem;
    public GameObject uiControll;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        dataItem = gameObject.GetComponent<inventoryManager>();
        uiControll = GameObject.FindWithTag("UIControll");

    }

    void Update()
    {
        if (!uiControll.GetComponent<UIControll>().isStay)
        {
            processInputs();
        }
        Animated();

        if (Input.GetMouseButtonDown(0) && !uiControll.GetComponent<UIControll>().isStay && !isAttack && !isMove)
        {

            AttackWeapon();
        }

    }

    void FixedUpdate()
    {

        Move();

        isMove = rb.velocity.magnitude > 0.1f;

        if (isShiftAttack) //Сдвиг при атаке (мб потом более оптимизировано сделать) Сделать под состояния???
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
