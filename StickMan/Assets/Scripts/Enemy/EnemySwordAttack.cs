using System;
using System.Collections;
using Manager;
using Player;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Enemy
{
    public class EnemySwordAttack : CombatAction
    {
        [SerializeField] private float KBForce = 5f;
        private Animator _animator;
        [SerializeField] private float newCooldownTime;
        [SerializeField] private bool isAttacking = false; // Thêm biến để theo dõi trạng thái tấn công
        [SerializeField] private float animationDuration;
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
        public bool IsAttacking
        {
            get => isAttacking;
            set => isAttacking = value;
        }
        void SetCooldownTime(float newTimeToCooldown)
        {
            base.cooldownTime = newTimeToCooldown;
        }
        void Start()
        {
            SetDamage(damage);
            SetCooldownTime(newCooldownTime);
            _animator = GetComponentInParent<Animator>();
        }

        public override void Attack()
        {
            if ((CanAttack) && !isAttacking && canMove )
            {
                CanMove = false;
                StartCoroutine(HandleAttackAnimation());
            }
        }
        // chém trúng thì trừ máu của người chơi 
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
                player.HealthControl.TakeDamage(damage);
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
            StartCoroutine(ResetCombatantState(player.PlayerSwordAttack));
           
        }

        public void StopEnemyAttack()
        {
            isAttacking = false;
            canMove = true;
        }

        // Coroutine to handle attack animation and cooldown
        private IEnumerator HandleAttackAnimation()
        {
            
            _animator.SetBool(AnimationStrings.canAttack, true); // Start attack animation
            isAttacking = true; // Mark as attacking
            yield return new WaitForSeconds(animationDuration); // Wait for animation duration
            _animator.SetBool(AnimationStrings.canAttack, false); // Stop attack animation
            isAttacking = false; // Reset attack state
            CanAttack = false; // Disable attack temporarily
            yield return new WaitForSeconds(cooldownTime); // Wait for cooldown
            CanAttack = true; // Re-enable attack
            
            CanMove = true; // Allow movement after attack
        }
        
        
    }
}
