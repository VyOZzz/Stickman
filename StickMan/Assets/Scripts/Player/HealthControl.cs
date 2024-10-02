using System.Collections;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class HealthControl : MonoBehaviour
    {
        [SerializeField] private PlayerCtrl playerCtrl;
        [SerializeField] private int HP = 100;
        [SerializeField] private Heathbar heathBar;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Animator animator;
        private AudioManager _audioManager;
        private bool isHit =false;
        private float hitCooldown = 0.5f;

        public int GetHP
        {
            get
            {
                return HP;
            }
        }
        private void Start()
        {
            heathBar.SetMaxHeath(HP);
            animator = GetComponentInParent<Animator>();
            _audioManager = FindFirstObjectByType<AudioManager>();
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;
            heathBar.SetHeath(HP);
            // play audio hurt
            HitHandle();
            _audioManager.PlaySFX("hurt");
            if (HP <= 0)
            {
                animator.SetBool(AnimationStrings.isDeath, true);
            }
            
        }
        
        //hàm này để tránh việc animation spamming
        private void HitHandle()
        {
            if (!isHit)
            {
                isHit = true;
                animator.SetTrigger(AnimationStrings.hitTrigger);
                StartCoroutine(HitCoroutine());
            }
        }
        IEnumerator HitCoroutine()
        {
            // di chuyển của player khi bị tấn công
            // can't attack when being hit
            playerCtrl.PlayerSwordAttack.CanMove = false;
            playerCtrl.PlayerSwordAttack.CanAttack = false;
            animator.SetBool(AnimationStrings.canMove, false);
            yield return new WaitForSeconds(hitCooldown);
            isHit = false;
            playerCtrl.PlayerSwordAttack.CanMove = true;
            //  tra lại trạng thái tấn công cho player khi bị đánh xong
            playerCtrl.PlayerSwordAttack.CanAttack = true;
            animator.SetBool(AnimationStrings.canMove, true);
        }
    }
}
