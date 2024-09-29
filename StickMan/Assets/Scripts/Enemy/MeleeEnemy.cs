using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private EnemySwordAttack enemySwordAttack;
        [SerializeField] private DetectionZone detectionZone;
        [FormerlySerializedAs("levelManager")] [SerializeField] private GameManager gameManager;
        private void Awake()
        {
            detectionZone = GetComponentInChildren<DetectionZone>();
            enemySwordAttack = GetComponentInChildren<EnemySwordAttack>();
            gameManager = FindFirstObjectByType<GameManager>();
        }
        private void Update()
        {
            if (detectionZone.HasTarget)
            {
                enemySwordAttack.Attack();            
            }
            else
            {
                // đặt lại trạng thái move khi player vút qua enemy mà enemy chưa kịp đánh 
                // kiểu nếu player đi qua enemy mà enemy chưa kịp đánh thì lúc dó enemy sẽ di chuyển vào hàm attack ở lớp 
                // ở script EnemySwordAttack thì CanMove = false. Và sẽ kdc chuyển về true nếu enemy k đánh trúng player.
                enemySwordAttack.CanMove = true;
            }
        }
        // ghi đè hàm Die nếu cần
        protected override void Die()
        {
            base.Die();
            gameManager.EnemyDefeated();
            Debug.Log("MeleeEnemy died in a special way!");
        }
    
    }
}
