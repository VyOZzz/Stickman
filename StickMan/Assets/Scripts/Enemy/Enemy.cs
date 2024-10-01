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
        [SerializeField]
        protected bool isBoss;
        private void Start()
        {
            heathBar = GetComponentInChildren<Heathbar>();
            heathBar.SetMaxHeath(HP);
            _animator = GetComponent<Animator>();
            //lấy maxhp để kiểm tra xem nó có vào rage state k 
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
            _animator.SetTrigger(AnimationStrings.hitTrigger);
            if (HP <= 0)
            {
                _animator.SetBool(AnimationStrings.isDeath, true);
                Die(); // Gọi phương thức để xử lý khi chết
            }
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
        public bool IsBoss()
        {
            return isBoss;
        }
    }
}