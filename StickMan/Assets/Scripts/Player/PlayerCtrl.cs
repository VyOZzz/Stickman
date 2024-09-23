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
    [SerializeField] private PlayerPunch playerPunch;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerJump PlayerJump => playerJump;
    public GroundChecker GroundChecker => groundChecker;
    public PlayerPunch PlayerPunch => playerPunch;
    private void Reset()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        groundChecker = FindObjectOfType<GroundChecker>();
        playerPunch = FindObjectOfType<PlayerPunch>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerPunch.Attack();
        }
    }
}
