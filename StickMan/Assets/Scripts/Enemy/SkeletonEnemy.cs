using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SkeletonEnemy : Enemy
{
   // [SerializeField] private GroundChecker groundChecker;
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        damageHandle.SetHP(100);
        SetSpeed(5);
        SetDamage(10);
       // groundChecker = FindObjectOfType<GroundChecker>();
    }

    public override void Move()
    {
        rigidbody2D.velocity = Vector2.left * speed;
    }

    public override void Attack()
    {
        
    }

    public override void ReceivedDamage()
    {
        damageHandle.TakeDamage(10);
    }

    private void Update()
    {
        Move();
        damageHandle.DieCheck();
    }
}
