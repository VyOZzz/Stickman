using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject victoryText;
    [SerializeField] private GameObject homeButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject retryButton;
    private void Awake()
    {
        //spawn enemy
        enemyManager.SpawnEnemies();
        // set active false
        victoryText.SetActive(false);
        gameOverText.SetActive(false);
        homeButton.SetActive(false);
        retryButton.SetActive(false);
        continueButton.SetActive(false);

        // Đặt Time.timeScale = 1 khi bắt đầu game
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (enemyManager.countEnemy <= 0)
        {
            StartCoroutine(DelayVictory());
        }
    }
    private void Victory()
    {
       
        victoryText.SetActive(true);
        homeButton.SetActive(true);
        retryButton.SetActive(true);
        continueButton.SetActive(true);
        Time.timeScale = 0;
        // timescale =0
        // set active true
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        homeButton.SetActive(true);
        retryButton.SetActive(true);
        continueButton.SetActive(true);
        // set active true
        // time.timescale = 0 de dung thoi gian
    }

    public void Continue()
    {
        
        Time.timeScale = 1;
        // tiep tuc timescale= 1
        // load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void Retry()
    {
        // timescale = 1 tiep tuc thoi gian
        // load lai scene nay
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnemyDefeated()
    {
        enemyManager.countEnemy--;
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator DelayVictory()
    {
        yield return new WaitForSeconds(1);
        Victory();
    }
}
