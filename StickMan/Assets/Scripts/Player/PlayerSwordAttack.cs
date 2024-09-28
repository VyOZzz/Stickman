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
    // isAttack để tránh việc gây sát thương k đúng lúc. 
    [SerializeField]private bool isAttack = false;
    [SerializeField] private float newCooldownTime = 0.5f;
    [SerializeField] private int comboStep;
    [SerializeField] private float lastTimeAttack = 0f;
    [SerializeField] private float comboWindow = 0.75f;
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
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
    }

    private void Update()
    {
        // Kiểm tra nếu hoạt ảnh tấn công đã kết thúc
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    
        // Giả sử bạn có các trạng thái animation với tên là "Attack1", "Attack2", "Attack3" cho combo
        if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
        {
            CanMove = false; // Trong khi tấn công thì không cho phép di chuyển
        }
        else
        {
            CanMove = true; // Khi hoạt ảnh tấn công kết thúc thì cho phép di chuyển
        }
        if (Time.time - lastTimeAttack > comboWindow && comboStep > 0)
        {
            comboStep = 0;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void Attack()
    {
        // animation attack 
        animator.SetTrigger(AnimationStrings.attackTrigger);
        //combostep handle
        ComboHandle();
        //cooldown
        StartCoroutine(AttackCooldown());
    }
    private void  OnTriggerEnter2D(Collider2D other)
    {
            if ( other.CompareTag("Enemy"))
            {
                HandleAttackOnEnemy(other);
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
            
            EnemySwordAttack enemySwordAttack = other.GetComponentInChildren<EnemySwordAttack>();
            
            if (enemyRb != null && enemySwordAttack != null)
            {
                // make enemy cannot attack when be attacked
                enemySwordAttack.StopEnemyAttack();
                // Calculate knockback direction
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                knockbackDirection.y = 1;
                // Apply knockback force
                enemyRb.AddForce(knockbackDirection * KBForce, ForceMode2D.Impulse);
            }

            StartCoroutine(ResetEnemyState(enemySwordAttack));
        }
    }

    private void ComboHandle()
    {
        lastTimeAttack = Time.time;
        if (comboStep == 0)
        {
            animator.SetInteger(AnimationStrings.comboStep, 1);
            comboStep = 1;
        }else if (comboStep == 1)
        {
            animator.SetInteger(AnimationStrings.comboStep, 2);
            comboStep = 2;
        }else if (comboStep == 2)
        {
            animator.SetInteger(AnimationStrings.comboStep, 3);
            comboStep = 3;
        }
    }

    
}
