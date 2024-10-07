using System.Collections;
using System.Collections.Generic;
using System.Data;
using Enemy;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerSwordAttack : CombatAction
    {
        private PlayerCtrl _playerCtrl;
        [SerializeField] private float KBForce = 5f;
        //private Animator animator;
        [HideInInspector] public int comboStep;
        [SerializeField] private float lastTimeAttack = 0f;
        private float comboWindow = 1f;
        private bool isCoolingdown = false;
        public  bool canMove = true;
        public  bool canAttack = true;
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
        void Start()
        {
            animator = GetComponentInParent<Animator>();
            _playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        private void Update()
        {
            
            // Kiểm tra nếu hoạt ảnh tấn công đã kết thúc
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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
                animator.SetInteger(AnimationStrings.comboStep, comboStep);
            }
        }
        public override void Attack()
        {
            //combostep handle
            if(!isCoolingdown)
            {
                ComboHandle();
            }
            else
            {
                Debug.Log("having cooldown ");
            }
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
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
                EnemySwordAttack enemySwordAttack = other.GetComponentInChildren<EnemySwordAttack>();
                if (enemyRb != null && enemySwordAttack != null)
                {
                    // Calculate knockback direction
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    knockbackDirection.y = 1;
                    // Apply knockback force
                    enemyRb.AddForce(knockbackDirection * KBForce, ForceMode2D.Impulse);
                }
                StartCoroutine(ResetCombatState(enemySwordAttack));
            }
        }
        private void ComboHandle()
        {
            if(comboStep < 3)
                lastTimeAttack = Time.time;
            if (comboStep == 0)
            {
                animator.SetTrigger(AnimationStrings.attackTrigger);
                animator.SetInteger(AnimationStrings.comboStep, 1);
                comboStep = 1;
            }else if (comboStep == 1)
            {
                animator.SetTrigger(AnimationStrings.attackTrigger);
                animator.SetInteger(AnimationStrings.comboStep, 2);
                comboStep = 2;
            }else if (comboStep == 2)
            {
                animator.SetTrigger(AnimationStrings.attackTrigger);
                animator.SetInteger(AnimationStrings.comboStep, 3);
                comboStep = 3;
            }else if (comboStep == 3)
            {
                // Không cho phép combo tiếp tục nếu đã đạt bước thứ 3
                comboStep = 0; // Reset combo sau khi cooldown xong
                animator.SetInteger(AnimationStrings.comboStep, 0); // Đặt lại animation
            }
        }
        public bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        
     
        private IEnumerator ResetCombatState(EnemySwordAttack enemySwordAttack)
        {
            // stun time equals  0.5f
            yield return new WaitForSeconds(0.5f);
            enemySwordAttack.CanMove = true;
        }

    }
}
