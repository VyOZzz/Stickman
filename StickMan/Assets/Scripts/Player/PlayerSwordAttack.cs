using Enemy;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerSwordAttack : CombatAction
    {
        private PlayerCtrl _playerCtrl;
        [SerializeField] private float KBForce = 5f;
        private Animator animator;
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
            SetCooldownTime(newCooldownTime);
            animator = GetComponentInParent<Animator>();
            _playerCtrl = GetComponentInParent<PlayerCtrl>();
        }

        private void Update()
        {
            
            // Kiểm tra nếu hoạt ảnh tấn công đã kết thúc
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
          
            
            // Giả sử bạn có các trạng thái animation với tên là "Attack1", "Attack2", "Attack3" cho combo
            if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                CanMove = false; // Trong khi tấn công thì không cho phép di chuyển
                if (stateInfo.normalizedTime >= 1.0f)
                {
                    comboStep = 0; // Reset comboStep khi hoạt ảnh hoàn thành
                    animator.SetInteger(AnimationStrings.comboStep, 0); // Reset animation state
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

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Attack()
        {
            // animation attack 
            
            
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
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                Debug.Log("take damage");
                enemy.TakeDamage(damage);
                animator.SetTrigger(AnimationStrings.hitTrigger);
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
                return;
            }
        }

        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        // private void HandleAttackSound()
        // {
        //     //audio when attack
        //     //điều kiện để khi mà player k thể attack thì k play sound
        //     if(!IsPointerOverUI()  // check nếu trỏ vào UI thì sẽ k play sound attack
        //        && CanAttack )
        //         FindAnyObjectByType<AudioManager>().PlaySFX("slash");
        // }
        
    }
}
