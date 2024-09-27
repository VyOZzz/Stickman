using System;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private PlayerCtrl _playerCtrl;
    private Animator _animator;
    private bool isFacingRight = true;
    private float _horInput;
    [SerializeField] private bool isWalk;

    [SerializeField] private bool isRun;
// sử dụng reset để chỉ việc reset là sẽ tự gán lại các component
    private void Reset()
    {
        LoadCtrl(out _playerCtrl);
    }
    private void LoadCtrl(out PlayerCtrl playerCtrl) => playerCtrl =  FindObjectOfType<PlayerCtrl>();
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
    }
    private void FixedUpdate()
    {
        if(_playerCtrl.PlayerSwordAttack.CanMove)
        {
            if (Input.GetKey(KeyCode.LeftShift) && _playerCtrl.GroundChecker.IsGrounded)
            {
                isRun = true;
            }
            else
            {
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
        rb.velocity = new Vector2(_horInput * currentSpeed , rb.velocity.y);
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
        if (Mathf.Abs(_horInput) > 0)
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
        currentSpeed = Mathf.Abs(rb.velocity.x);
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
