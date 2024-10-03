using System.Collections;
using Manager;
using UnityEngine;

namespace Enemy
{
    public partial class Enemy : MonoBehaviour
    {
        private int MaxHP;
        public int HP;
        private Heathbar heathBar;
        private Animator _animator;
        private bool isHit = false;
        [SerializeField] private float hitCooldown  = 0.5f;
        [SerializeField]
        protected bool isBoss = false;
        private void Start()
        {
            heathBar = GetComponentInChildren<Heathbar>();
            heathBar.SetMaxHeath(HP);
            _animator = GetComponent<Animator>();
            MaxHP = HP;
        }

        public void SetHP(int hp) // Phương thức để thiết lập HP
        {
            HP = hp;
        }
        // public vì có thể có nhiều cái khác gây sát thương ra k chỉ enemy
        public void TakeDamage(int damage)
        {
            HP -= damage;
            heathBar.SetHeath(HP);
            HitHandle();
            if (HP <= 0)
            {
                _animator.SetBool(AnimationStrings.isDeath, true);
            }
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
        private void HitHandle()
        {
            if (!isHit)
            {
                isHit = true;
                _animator.SetTrigger(AnimationStrings.hitTrigger);
                StartCoroutine(HitCoroutine());
            }
        }
        IEnumerator HitCoroutine()
        {
            yield return new WaitForSeconds(hitCooldown);
            isHit = false;
        }

        public bool IsBoss()
        {
            return isBoss;
        }

        public bool IsHealthUnder40Percent()
        {
            return HP < MaxHP * 0.4f;
        }
    }
}