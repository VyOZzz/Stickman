using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MeleeEnemy : Enemy
{
    [SerializeField] private EnemySwordAttack enemySwordAttack;
    [SerializeField] private DetectionZone detectionZone;
    [SerializeField] private LevelManager levelManager;
    private void Awake()
    {
        detectionZone = GetComponentInChildren<DetectionZone>();
        enemySwordAttack = GetComponentInChildren<EnemySwordAttack>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Update()
    {
        if (detectionZone.HasTarget)
        {
            enemySwordAttack.Attack();            
        }
    }
    // ghi đè hàm Die nếu cần
    protected override void Die()
    {
        base.Die();
        levelManager.EnemyDefeated();
        Debug.Log("MeleeEnemy died in a special way!");
    }
    
}
