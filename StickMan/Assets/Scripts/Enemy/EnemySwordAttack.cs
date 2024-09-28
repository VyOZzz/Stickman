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
    [SerializeField] private float newCooldownTime = 0.5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isAttacking = false; // Thêm biến để theo dõi trạng thái tấn công

    public bool CanMove
    {
        get => canMove;
        set
        {
            value = canMove;
        }
    }
    public bool CanAttack
    {
        get => canAttack;
        set => canAttack = value;
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
        if (canAttack && !isAttacking && canMove )
        {
            CanMove = false;
            isAttacking = true; // theo dõi trạng thái tấn công
            // animation attack
            animator.SetTrigger(AnimationStrings.attackTrigger);
            //cooldown
            StartCoroutine(AttackCooldown());
        }
    }
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.CompareTag("Player"))
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
            player.PlayerSwordAttack.CanAttack = false;
            player.PlayerSwordAttack.CanMove = false;
            if (rb != null)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                knockbackDirection.y = 1;
                // Apply knockback force
                rb.AddForce(knockbackDirection * KBForce * 1.5f , ForceMode2D.Impulse);
            }
        }
        // khôi phục khả năng di chuyển và tấn công của player
        StartCoroutine(ResetPlayerState(player.PlayerSwordAttack));
        CanMove = true; // khôi phục khả năng di chuyển của enemy sau khi tấn công
        isAttacking = false; // khi enemy đánh player xong thì isAttacking chuyển về false để đánh tiếp 
    }

    public void StopEnemyAttack()
    {
        isAttacking = false;
        canMove = true;
    }
    
}
