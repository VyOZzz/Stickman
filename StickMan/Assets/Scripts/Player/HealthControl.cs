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
        }
        public void TakeDamage(int damage)
        {
            Debug.Log(HP);
            HP -= damage;
            heathBar.SetHeath(HP);
            if (HP <= 0)
                Die();
        }
        private void Die()
        {
        
            StartCoroutine(DieDelay());
        }
        IEnumerator DieDelay()
        {
            animator.SetBool(AnimationStrings.isDeath, true);
            yield return new WaitForSeconds(1);
            Destroy(playerCtrl.gameObject);
            gameManager.GameOver();
        }
    }
}
