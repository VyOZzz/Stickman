using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private MeleeEnemy _meleeEnemy;
    private bool hasTarget = false;
    private Animator _animator;
    public bool HasTarget
    {
        get => hasTarget;
        set
        {
            hasTarget = value;
        }
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
