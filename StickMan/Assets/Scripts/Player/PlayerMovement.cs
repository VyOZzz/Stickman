using Manager;
using UnityEngine;
using UnityEngine.InputSystem;
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
            if (_animator.GetBool(AnimationStrings.isDeath) || playerCtrl.Dash.IsDashing) return;
#if UNITY_ANDROID
            
            _horInput = playerCtrl.Joystick.Horizontal;
#endif
#if UNITY_WINDOWS || UNITY_EDITOR
            {
                _horInput = Input.GetAxis("Horizontal");
            }
#endif
            if (Mathf.Abs(_horInput)> 0.2f && playerCtrl.GroundChecker.IsGrounded)
            {
                isRun = true;
                // không thể attack khi chạy
                playerCtrl.PlayerSwordAttack.CanAttack = false;
                isWalk = false;
            }
            else
            {
                // trả lại trạng thái có thể tấn công khi  không chạy nữa
                playerCtrl.PlayerSwordAttack.CanAttack = true;
                isRun = false;
            }

            if (!playerCtrl.PlayerSwordAttack.CanMove) return;
            
            WalkHandle();
            AnimationMovementHandle();
        }
        private void WalkHandle()
        {
            currentSpeed = isRun ? runSpeed : walkSpeed;
            // di chuyển
            if (!float.IsNaN(_horInput))
            {
                // set animation walk
                WalkState();
                
                FlipDirection();
                rb.linearVelocity = new Vector2(_horInput * currentSpeed, rb.linearVelocity.y);
            }
            else
            {
                Debug.LogError("Invalid horizontal input detected! _horInput is NaN.");
            }
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
        // trạng thái chạy, idle, đi bộ 
        private void AnimationMovementHandle()
        {
            currentSpeed = Mathf.Abs(rb.linearVelocity.x);
            if (currentSpeed <= 0)
            {
                _animator.SetBool(AnimationStrings.isWalk, false);
                _animator.SetBool(AnimationStrings.isIdle, true);
                _animator.SetBool(AnimationStrings.isRun, false);
            }else if (currentSpeed > 0 && currentSpeed <= walkSpeed )
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
