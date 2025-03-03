using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterControllerW : MonoBehaviour
{
    public Animator anim;

    public float speed;

    public Rigidbody2D rb;

    float ForwardBehind;
    float LeftRight;

    private Vector2 moveD;

    private Vector2 LastmoveD;


    void Start()
    {
        
    }

    void Update()
    {
        processInputs();
        //Animated();

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

    void Move()
    {
        rb.velocity = new Vector2(moveD.x * speed, moveD.y * speed); 
    }

    /*void Animated()
    {
        anim.SetFloat("RouteFB", moveD.y);
        anim.SetFloat("RouteLR", moveD.x);
        anim.SetFloat("moveD", moveD.magnitude);
        anim.SetFloat("LastmoveDx", LastmoveD.x);
        anim.SetFloat("LastmoveDy", LastmoveD.y);
    }*/
}
