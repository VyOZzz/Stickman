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
        public int comboStep;
        [SerializeField] private float lastTimeAttack = 0f;
        private float comboWindow = 2f;
        private bool isCoolingdown = false;
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
                if (stateInfo.normalizedTime >= 0.99f) // diều kiện nếu nó hoàn thành animation rồi 
                { // 0.99 vì aniamtion nó có giao động khi comboattak
                        // comboStep = 0; // Reset comboStep khi hoạt ảnh hoàn thành
                        // animator.SetInteger(AnimationStrings.comboStep, 0); // Reset animation state
                        StartCoroutine(StartCooldown());
                }
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
                    // make enemy cannot attack when be attacked
                    enemySwordAttack.StopEnemyAttack();
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
            { // Không cho phép combo tiếp tục nếu đã đạt bước thứ 3
                if (Time.time - lastTimeAttack >= base.cooldownTime) // Kiểm tra cooldown
                {
                    comboStep = 0; // Reset combo sau khi cooldown xong
                    animator.SetInteger(AnimationStrings.comboStep, 0); // Đặt lại animation
                }
            }
        }
        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void HandleAttackSound()
        {
            //audio when attack
            //điều kiện để khi mà player k thể attack thì k play sound
            if(!IsPointerOverUI()  // check nếu trỏ vào UI thì sẽ k play sound attack
               && CanAttack )
                FindAnyObjectByType<AudioManager>().PlaySFX("slash");
        }
        private IEnumerator StartCooldown()
        {
            if (!isCoolingdown)
            {
                Debug.Log("iscoolingdown");
                isCoolingdown = true;
                canAttack = false;
                animator.SetBool(AnimationStrings.canAttack, false);
                yield return new WaitForSeconds(cooldownTime); // chờ cooldown
                isCoolingdown = false;
                canAttack = true;
                animator.SetBool(AnimationStrings.canAttack, canAttack);
                comboStep = 0; // Reset combo after cooldown
                animator.SetInteger(AnimationStrings.comboStep, 0);
            }
        }
        private IEnumerator ResetCombatState(EnemySwordAttack enemySwordAttack)
        {
            // stun time equals  0.5f
            yield return new WaitForSeconds(0.5f);
            enemySwordAttack.CanMove = true;
        }

    }
}
