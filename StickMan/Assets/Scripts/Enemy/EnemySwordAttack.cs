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
        private float KBForce = 5f;
        private Animator _animator;
        [SerializeField] private float newCooldownTime;
        [SerializeField] private bool isAttacking = false; // Thêm biến để theo dõi trạng thái tấn công
        [SerializeField] private float animationDuration;
        private float timeSceneLastAttack;
        public new bool canMove = true;
        public new bool canAttack = true;
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
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                //knockback
                if (rb != null)
                {
                    // Calculate knockback direction
                    Vector2 knockbackDirection = (player.transform.position - transform.position);
                    knockbackDirection.y = 0;
                    // Apply knockback force
                    rb.AddForce(knockbackDirection * KBForce * 1.5f , ForceMode2D.Impulse);
                }
            }
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
            timeSceneLastAttack = 0f;  //
            yield return new WaitForSeconds(animationDuration); // Wait for animation duration
            _animator.SetBool(AnimationStrings.canAttack, false); // Stop attack animation
            isAttacking = false; // Reset attack state
            CanAttack = false; // Disable attack temporarily
            while (timeSceneLastAttack < newCooldownTime)
            {
                timeSceneLastAttack += Time.deltaTime;
                yield return null; // nó sẽ bỏ qua frame này và chờ frame tiếp theo và nó lại tiếp tục vòng whiile
            }
            CanAttack = true; // Re-enable attack
            CanMove = true; // Allow movement after attack
        }
    }
}
