using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasic : MonoBehaviour
{

    public float speed;

    public Rigidbody2D rb;

    private Vector2 moveDirecton;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirecton = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirecton.x * speed, moveDirecton.y * speed);
    }

}