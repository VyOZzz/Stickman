using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        SetHP(50); // Gán HP cho Slime
        speed = 5; // Gán tốc độ
    }

    public override void Move()
    {
        // Logic di chuyển
        rigidbody2D.velocity = Vector2.left * speed;
    }
    
    public override void Attack()
    {
        // Logic tấn công
    }

    private void Update()
    {
    }
}