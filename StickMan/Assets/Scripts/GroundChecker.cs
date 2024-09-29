using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance = 0.2f;
    private void Reset()
    {
        LoadCtrl(out playerCtrl);
    }

    [Obsolete("Obsolete")]
    private void LoadCtrl(out PlayerCtrl  newPlayerCtrl) => 
        newPlayerCtrl = FindAnyObjectByType<PlayerCtrl>();

    public bool IsGrounded
    {
        get => isGround;
        set
        {
            isGround = value;
            playerCtrl.animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    private void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        GroundChecking();
        
    }

    private void GroundChecking()
    {
        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down;
        bool isHit = Physics2D.Raycast(startPos, direction, maxDistance, layerMask);
        if (isHit)
        {
            IsGrounded = true;
        }
        else
            IsGrounded = false;
    }
    
    
}
