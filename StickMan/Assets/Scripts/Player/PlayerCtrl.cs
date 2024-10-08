using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
#if UNITY_ANDROID
    joystick.gameObject.SetActive(true);
    Debug.Log("Joystick enabled for Android.");
#else
            joystick.gameObject.SetActive(false);
            Debug.Log("Joystick disabled for non-Android platforms.");
#endif
        }
        private void Update()
        {
#if  UNITY_STANDALONE || UNITY_EDITOR
            if (IsPointerOverUI()) return;
            if(groundChecker.IsGrounded && Input.GetMouseButtonDown(0))
            {
                playerSwordAttack.Attack();
            }
            if(Dash.CanDash && Input.GetKeyDown(KeyCode.LeftShift))
                StartCoroutine(dash.DashCoroutine());
#endif
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
            Debug.Log("Attack button pressed.");
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
        public bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
