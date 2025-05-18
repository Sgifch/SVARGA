using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterControllerW : MonoBehaviour
{
    //���������
    public Sprite spriteInGrass;

    //�������� � ������
    public Animator anim;

    public float speed;

    public Rigidbody2D rb;

    float ForwardBehind;
    float LeftRight;

    private Vector2 moveD;

    private Vector2 LastmoveD;

    //������ � ������
    public characterStat stat;

    //������ �������� � ����
    public float changeHealthPoint;
    public float changeMannaPoint;

    public Image imageHealthBar;
    public Image imageMannaBar;

    //������� �� ������� �����
    public bool enemyTouch;

    //�����
    private float time = 0f;

    Collision2D collider = null;
    void Start()
    {
        enemyTouch = false;
    
        changeHealthPoint = (float)(stat.healthPoint) / (stat.maxHealthPoint);
        changeMannaPoint = (float)(stat.mannaPoint) / (stat.maxMannaPoint);

        imageHealthBar.fillAmount = changeHealthPoint;
        imageMannaBar.fillAmount = changeMannaPoint;

        //�������� � ������������� ������� �� ���� ��������� ��� Awake
    }

    void Update()
    {
        time += Time.deltaTime;
    
        processInputs();
        //Move();
        //Animated();

        if (enemyTouch)
        {
            if (collider.gameObject.tag == "enemyStatic")
            {
                Attack();
            }
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

    //������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyTouch = true;
        collider = collision;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyTouch = false;
        collider = collision;
    }

    //�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "grass")
        {
            ChangeSprite(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void Move()
    {
        rb.velocity = new Vector2(moveD.x * speed, moveD.y * speed); 
    }

    void Attack()
    {
        enemyProfile attackPoint = collider.gameObject.GetComponent<enemy>().enemyStat;  //��� ����� ���������� ��� ������ ������ � �� �����
        float intervalTime = collider.gameObject.GetComponent<enemy>().intervalAttack;

        if (time >= intervalTime)
        {
            stat.healthPoint = stat.healthPoint - attackPoint.attack;

            changeHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
            imageHealthBar.fillAmount = changeHealthPoint;
            time = 0;

            print("Attack");
        }
        
    }

    public void UseFood(inventorySlot useItem)
    {
        foodItem food = (foodItem)useItem.item; //����� �������������� � ������
        int recoveryHealth = food.healthAmount;
        stat.healthPoint = stat.healthPoint + recoveryHealth;

        ChangeHealth();
    }

    public void ChangeHealth()
    {
        changeHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
        imageHealthBar.fillAmount = changeHealthPoint;
    }

    /*void Animated()
    {
        anim.SetFloat("RouteFB", moveD.y);
        anim.SetFloat("RouteLR", moveD.x);
        anim.SetFloat("moveD", moveD.magnitude);
        anim.SetFloat("LastmoveDx", LastmoveD.x);
        anim.SetFloat("LastmoveDy", LastmoveD.y);
    }*/

    public void ChangeSprite(bool change)
    {
        if (change)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteInGrass;
        }
    }
}
