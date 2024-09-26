using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Rigidbody2D rb;
    private Animator _animator;

    // sử dụng reset để khi bị quên k gắn 1 componet nào đó thì chỉ việc reset lại là dc.
    private void Reset()
    {
        LoadCtrl(out playerCtrl);
    }

    private void LoadCtrl(out PlayerCtrl playerCtrl) => playerCtrl =  FindObjectOfType<PlayerCtrl>();

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
        Debug.Log(_animator != null ? "Animator found" : "Animator not found");
    }
    void Update()
    {
        // nếu player ở mặt đất thì mới jump được
        // nhận input từ người chơi
        if (Input.GetKeyDown(KeyCode.Space) && playerCtrl.GroundChecker.IsGrounded)
        {
            if(playerCtrl.SwordAttack.CanMove)
                JumpHandle();
        }
    }
    void JumpHandle()
    {
        Debug.Log("jump");
        //animation nhay
        _animator.SetTrigger(AnimationStrings.jump);
        // nhảy
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        
        
        
    }
}
