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
        [FormerlySerializedAs("levelManager")] [SerializeField] private GameManager gameManager;
        [SerializeField] private Animator animator;
        private AudioManager _audioManager;
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
            Debug.Log(HP);
            HP -= damage;
            heathBar.SetHeath(HP);
            if(HP > 0) //điều kiện để khi player chết sẽ không phát ra sound hurt 
                _audioManager.PlaySFX("hurt");
            if (HP <= 0)
            {
                animator.SetBool(AnimationStrings.isDeath, true);
            }
        }
    }
}
