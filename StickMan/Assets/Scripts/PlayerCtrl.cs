using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCtrl : MonoBehaviour
{
    [FormerlySerializedAs("_playerMovement")] [SerializeField]private PlayerMovement playerMovement;
    [FormerlySerializedAs("_groundChecker")] [SerializeField]private GroundChecker groundChecker;
    [FormerlySerializedAs("_playerJump")] [SerializeField]private PlayerJump playerJump;

    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerJump PlayerJump => playerJump;
    public GroundChecker GroundChecker => groundChecker;

    private void Reset()
    {
        playerJump = FindObjectOfType<PlayerJump>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        groundChecker = FindObjectOfType<GroundChecker>();
    }
    
   
}
