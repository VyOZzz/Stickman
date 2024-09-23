using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : CombatAction
{
    [SerializeField]private new int _damage;
    [SerializeField] private Animator animator;
    void Start()
    {
        SetDamage(_damage);
        cooldownTime = 0.5f;
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack()
    {
        if (canAttack)
        {
            // animation punch
            animator.SetTrigger("Punch");
            // xử lý damage 
            //cooldown
            StartCoroutine(AttackCooldown());
            
        }
    }
}
