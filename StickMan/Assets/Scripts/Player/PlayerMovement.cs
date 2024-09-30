using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float currentSpeed = 5f;
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 10f;
        private PlayerCtrl playerCtrl;
        private Animator _animator;
        private bool isFacingRight = true;
        private float _horInput;
        [SerializeField] private bool isWalk;
        [SerializeField] private bool isRun;

        public bool IsWalk => isWalk;

        public bool IsRun => isRun;
        
// sử dụng reset để chỉ việc reset là sẽ tự gán lại các component
        private void Reset()
        {
            LoadCtrl(out playerCtrl);
        }
        private void LoadCtrl(out PlayerCtrl newPlayerCtrl) => 
            newPlayerCtrl =  FindFirstObjectByType<PlayerCtrl>();
        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody2D>();
            _animator = GetComponentInParent<Animator>();
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }
        private void FixedUpdate()
        {
            if(playerCtrl.PlayerSwordAttack.CanMove == true)
            {
                if (Input.GetKey(KeyCode.LeftShift) && playerCtrl.GroundChecker.IsGrounded)
                {
                    isRun = true;
                    // không thể attack khi chạy
                    playerCtrl.PlayerSwordAttack.CanAttack = false;
                    isWalk = false;
                }
                else
                {
                    // trả lại trạng thái có thể tấn công khi hết run
                    playerCtrl.PlayerSwordAttack.CanAttack = true;
                    isRun = false;
                }
                WalkHandle();
            }
            AnimationMovementHandle();
        }
        private void WalkHandle()
        {
            currentSpeed = isRun ? runSpeed : walkSpeed;
            // nhận input từ người chơi với các phím như AD hay mũi tên
            _horInput = Input.GetAxis("Horizontal");
            WalkState();
            // set animation walk
            FlipDirection();
            // di chuyển
            rb.linearVelocity = new Vector2(_horInput * currentSpeed , rb.linearVelocity.y);
        }
        private void FlipDirection()
        {
            // lớn hơn 0.1 để nó không tự động quay sang bên phải khi nó về 0
            if (_horInput > 0.1)
            {
                if (!isFacingRight)
                {
                    transform.parent.localScale *= new Vector2(-1, 1);
                    isFacingRight = true;
                }
            }
            else if(_horInput < 0)
            {
                if (isFacingRight)
                {
                    isFacingRight = false;
                    transform.parent.localScale *= new Vector2(-1, 1);
                }
            }
        }
        private void WalkState()
        {
            if (Mathf.Abs(_horInput) > 0 && playerCtrl.GroundChecker.IsGrounded)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;
            }
        }

        private void AnimationMovementHandle()
        {
            currentSpeed = Mathf.Abs(rb.linearVelocity.x);
            if (currentSpeed <= 0)
            {
                _animator.SetBool(AnimationStrings.isWalk, false);
                _animator.SetBool(AnimationStrings.isIdle, true);
                _animator.SetBool(AnimationStrings.isRun, false);
            }else if (currentSpeed > 0 && currentSpeed <= walkSpeed)
            {
                _animator.SetBool(AnimationStrings.isIdle, false);
                _animator.SetBool(AnimationStrings.isWalk, true);
                _animator.SetBool(AnimationStrings.isRun, false);
            }
            else if (currentSpeed > walkSpeed)
            {
                _animator.SetBool(AnimationStrings.isIdle, false);
                _animator.SetBool(AnimationStrings.isWalk, false);
                _animator.SetBool(AnimationStrings.isRun, true);
            }
        }
    }
}
