using Manager;
using Player;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private PlayerCtrl _playerCtrl;
    private AudioManager _audioManager;
    private float timePerStep = 0.5f;
    private float footStepsTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCtrl = GetComponentInParent<PlayerCtrl>();
        _audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // check iswalk là có cả check ground luôn
        //check nếu player đang đi và đang ở mặt đất thì play sound
        if (_playerCtrl.PlayerMovement.IsWalk)
        {
            // cooldown khoảng thời gian 
            footStepsTimer -= Time.deltaTime;
            if(_playerCtrl.PlayerMovement.IsRun)
            {
                if (footStepsTimer <= 0f)
                {
                    _audioManager.PlaySFX("run");
                    footStepsTimer = timePerStep;
                }
            }else if (_playerCtrl.PlayerMovement.IsWalk)
            {
                if (footStepsTimer <= 0f)
                {
                    _audioManager.PlaySFX("walk");
                    footStepsTimer = timePerStep;
                }
            }
        }
    }
}
