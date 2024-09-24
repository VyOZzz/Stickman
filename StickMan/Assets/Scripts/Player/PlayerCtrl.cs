using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private GroundChecker groundChecker;
    [SerializeField]private PlayerJump playerJump;
    [SerializeField] private SwordAttack swordAttack;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerJump PlayerJump => playerJump;
    public GroundChecker GroundChecker => groundChecker;
    public SwordAttack SwordAttack => swordAttack;
    private void Reset()
    {
        swordAttack = FindObjectOfType<SwordAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        groundChecker = FindObjectOfType<GroundChecker>();
        playerJump = FindObjectOfType<PlayerJump>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            swordAttack.Attack();
        }
    }
}
