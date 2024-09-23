using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : PlayerAttack
{
    [SerializeField] private Animator animator;
    void Start()
    {
        SetDamage(5);
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
