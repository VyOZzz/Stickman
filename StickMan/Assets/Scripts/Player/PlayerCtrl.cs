using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private GroundChecker groundChecker;
    [SerializeField]private PlayerJump playerJump;
    [FormerlySerializedAs("swordAttack")] [SerializeField] private PlayerSwordAttack playerSwordAttack;
    [FormerlySerializedAs("_animator")] public Animator animator;
    [SerializeField] private HealthControl healthControl;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerJump PlayerJump => playerJump;
    public GroundChecker GroundChecker => groundChecker;
    public PlayerSwordAttack PlayerSwordAttack => playerSwordAttack;
    public HealthControl HealthControl => healthControl;
    
    private void Reset()
    {
        playerSwordAttack = FindObjectOfType<PlayerSwordAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        groundChecker = FindObjectOfType<GroundChecker>();
        playerJump = FindObjectOfType<PlayerJump>();
        healthControl = FindObjectOfType<HealthControl>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerSwordAttack.Attack();
        }
    }

}
