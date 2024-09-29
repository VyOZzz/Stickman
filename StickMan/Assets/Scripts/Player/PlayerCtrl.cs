using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerCtrl : MonoBehaviour
    {
        [SerializeField]private PlayerMovement playerMovement;
        [SerializeField]private GroundChecker groundChecker;
        [SerializeField]private PlayerJump playerJump;
        [FormerlySerializedAs("swordAttack")] [SerializeField] private PlayerSwordAttack playerSwordAttack;
        [FormerlySerializedAs("_animator")] public Animator animator;
        [SerializeField] private HealthControl healthControl;
        public PlayerMovement PlayerMovement => playerMovement;
        public PlayerJump PlayerJump => playerJump;
        public GroundChecker GroundChecker => groundChecker;
        public PlayerSwordAttack PlayerSwordAttack => playerSwordAttack;
        public HealthControl HealthControl => healthControl;
    
        private void Reset()
        {
            playerSwordAttack = FindFirstObjectByType<PlayerSwordAttack>();
            playerMovement = FindFirstObjectByType<PlayerMovement>();
            groundChecker = FindFirstObjectByType<GroundChecker>();
            playerJump = FindFirstObjectByType<PlayerJump>();
            healthControl = FindFirstObjectByType<HealthControl>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && groundChecker.IsGrounded && playerSwordAttack.CanAttack)
            {
                playerSwordAttack.Attack();
            }
        }

    }
}
