using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerCtrl : MonoBehaviour
    {
        [SerializeField]private PlayerMovement playerMovement;
        [SerializeField]private GroundChecker groundChecker;
        [FormerlySerializedAs("swordAttack")] [SerializeField] private PlayerSwordAttack playerSwordAttack;
        [FormerlySerializedAs("_animator")] public Animator animator;
        [SerializeField] private HealthControl healthControl;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Joystick joystick;
        [SerializeField] private Dash dash;
        public Dash Dash => dash;
        public Joystick Joystick => joystick;
        public PlayerMovement PlayerMovement => playerMovement;
        public GroundChecker GroundChecker => groundChecker;
        public PlayerSwordAttack PlayerSwordAttack => playerSwordAttack;
        public HealthControl HealthControl => healthControl;
        public GameManager GameManager => gameManager;
        
        
        private void Reset()
        {
            playerSwordAttack = FindFirstObjectByType<PlayerSwordAttack>();
            playerMovement = FindFirstObjectByType<PlayerMovement>();
            groundChecker = FindFirstObjectByType<GroundChecker>();
            healthControl = FindFirstObjectByType<HealthControl>();
            gameManager = FindFirstObjectByType<GameManager>();
            joystick = FindFirstObjectByType<Joystick>();
            dash = FindFirstObjectByType<Dash>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (playerSwordAttack.IsPointerOverUI()) return;
            
            if(groundChecker.IsGrounded && Input.GetMouseButtonDown(0))
            {
                playerSwordAttack.Attack();
            }
            if(Dash.CanDash && Input.GetKeyDown(KeyCode.LeftShift))
                StartCoroutine(dash.DashCoroutine());
        }
        public void PlaySlashSound()
        {
            FindAnyObjectByType<AudioManager>().PlaySFX("slash");
        }

        public void Die()
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }

        public void AttackButton()
        {
#if UNITY_ANDROID
            if(groundChecker.IsGrounded )
            {
                playerSwordAttack.Attack();
            }
#endif
        }

        public void DashButton()
        {
#if UNITY_ANDROID
            if(Dash.CanDash )
                StartCoroutine(dash.DashCoroutine());
#endif
        }
    }
}
