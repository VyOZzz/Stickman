using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordAttack : CombatAction
{
    [SerializeField]private new int _damage;
    [SerializeField] private Animator animator;
    void Start()
    {
        SetDamage(_damage);
        cooldownTime = 0.5f;
        animator = GetComponentInParent<Animator>();
    }
    
    public override void Attack()
    {
        if (canAttack)
        {
            // animation punch
            animator.SetTrigger(AnimationStrings.attackTrigger);
            // xử lý damage 
            //cooldown
            StartCoroutine(AttackCooldown());
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("take damage");
                enemy.TakeDamage(_damage);
            }
        }
    }
    
}
