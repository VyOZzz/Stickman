using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private SwordAttack swordAttack;
    [SerializeField] private DetectionZone detectionZone;
    [SerializeField] private LevelManager levelManager;
    private void Awake()
    {
        detectionZone = GetComponentInChildren<DetectionZone>();
        swordAttack = GetComponentInChildren<SwordAttack>();
        swordAttack.isEnemy = true;
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Update()
    {
        if (detectionZone.HasTarget)
        {
            swordAttack.Attack();
            
        }
    }
    // ghi đè hàm Die nếu cần
    protected override void Die()
    {
        base.Die();
        Debug.Log("MeleeEnemy died in a special way!");
    }
    
}
