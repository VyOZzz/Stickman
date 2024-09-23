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
    [FormerlySerializedAs("playerPunch")] [SerializeField] private Punch punch;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerJump PlayerJump => playerJump;
    public GroundChecker GroundChecker => groundChecker;
    public Punch Punch => punch;
    private void Reset()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        groundChecker = FindObjectOfType<GroundChecker>();
        punch = FindObjectOfType<Punch>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            punch.Attack();
        }
    }
}
