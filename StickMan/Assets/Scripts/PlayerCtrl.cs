using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private PlayerJump _playerJump;
    // Start is called before the first frame update
    void Start()
    {
        _playerJump =  GetComponent<PlayerJump>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
