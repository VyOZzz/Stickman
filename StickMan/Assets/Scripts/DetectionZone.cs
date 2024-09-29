using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Manager;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private MeleeEnemy _meleeEnemy;
    private bool _hasTarget = false;
    private Animator _animator;
    public bool HasTarget
    {
        get => _hasTarget;
        set => _hasTarget = value;
    }
    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _meleeEnemy = GetComponentInParent<MeleeEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hello");
            HasTarget = true;
            _animator.SetBool(AnimationStrings.hasTarget, HasTarget);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HasTarget = false;
            _animator.SetBool(AnimationStrings.hasTarget, HasTarget);
        }
    }
}
