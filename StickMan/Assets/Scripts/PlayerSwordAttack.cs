using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerSwordAttack : CombatAction
{
    [SerializeField] private float KBForce = 5f;
    [SerializeField]private new int _damage = 10;
    private Animator animator;
    [SerializeField]private bool isAttack = false;
    [SerializeField] private float newCooldownTime = 0.5f;
    [SerializeField] private bool canMove = true;

    public bool CanMove
    {
        get => canMove;
        set
        {
            value = canMove;
        }
    }
    
    void SetCooldownTime(float _newCooldownTime)
    {
        cooldownTime = newCooldownTime;
    }
    void Start()
    {
        SetDamage(_damage);
        SetCooldownTime(newCooldownTime);
        animator = GetComponentInParent<Animator>();
    }
    public override void Attack()
    {
        if (canAttack)
        {
            CanMove = false;
            isAttack = true;
            // animation attack
            animator.SetTrigger(AnimationStrings.attackTrigger);
            Debug.Log("here");
            //cooldown
            StartCoroutine(AttackCooldown());
        }
    }
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if(isAttack)
        {
            if ( other.CompareTag("Enemy"))
            {
                HandleAttackOnEnemy(other);
            }
        }
    }
    private void HandleAttackOnEnemy(Collider2D other)
    {
        // nếu có script Enemy thì mới tấn công được
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("take damage");
            enemy.TakeDamage(_damage);
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                knockbackDirection.y = 1;
                // Apply knockback force
                enemyRb.AddForce(knockbackDirection * KBForce, ForceMode2D.Impulse);
            }
        }
    }
}
