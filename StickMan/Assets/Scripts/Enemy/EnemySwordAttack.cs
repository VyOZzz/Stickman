using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySwordAttack : CombatAction
{
    [SerializeField] private float KBForce = 5f;
    [SerializeField]private new int _damage = 10;
    private Animator animator;
    [SerializeField] private DetectionZone detectionZone;
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
        detectionZone = GetComponentInChildren<DetectionZone>();
    }
    public override void Attack()
    {
        if (canAttack)
        {
            CanMove = false;
            isAttack = true;
            // animation attack
            animator.SetTrigger(AnimationStrings.attackTrigger);
            //cooldown
            StartCoroutine(AttackCooldown());
        }
    }
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if(isAttack && other.gameObject.CompareTag("Player"))
        {
            HandleAttackOnPlayer(other);
        }
    }

    private void HandleAttackOnPlayer(Collider2D other)
    {
        
        var player = other.gameObject.GetComponent<PlayerCtrl>();
        if (player != null)
        {
            Debug.Log("HIT");
            player.HealthControl.TakeDamage(_damage);
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                knockbackDirection.y = 1;
                // Apply knockback force
                rb.AddForce(knockbackDirection * KBForce * 1.5f , ForceMode2D.Impulse);
            }
        }

        isAttack = false;
        CanMove = true;
    }
}
