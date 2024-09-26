using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject finishPanel;
    private int enemyCount;
    private void Start()
    {
        // lay current index bang cach buildinindex
        //spawn enemy
        // set active false
        
    }

    private void LevelCompleted()
    {
        // timescale =0
        // set active true
    }
    public void GameOver()
    {
        // set active true
        // time.timescale = 0 de dung thoi gian
    }

    public void Continue()
    {
        // tiep tuc timescale= 1
        // load next level
    }

    public void Retry()
    {
        // timescale = 1 tiep tuc thoi gian
        // load lai scene nay
    }
}
