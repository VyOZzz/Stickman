using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance = 0.1f;
    private void Reset()
    {
        LoadCtrl(out playerCtrl);
    }

    private void LoadCtrl(out PlayerCtrl playerCtrl) => playerCtrl = FindObjectOfType<PlayerCtrl>();
    public bool IsGrounded
    {
        get => isGround;
        set => isGround = value;
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
