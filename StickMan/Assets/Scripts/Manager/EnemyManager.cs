using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;
    public int countEnemy;
    public int enemyAlive;
    [SerializeField] private int countWaves;
    [SerializeField] private int currentLevel;
    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex -1; // trừ 1 vì index của list bắt đầu từ 0
        countEnemy = levelDatas[currentLevel].totalEnemies;
        StartCoroutine(SpawnEnemies());
        
    }

    IEnumerator SpawnEnemies()
    {
        while (countWaves <levelDatas[currentLevel].numberWave && currentLevel >= 0 ) 
            // check currentlevel bởi vì nếu scene0 thì k phả scene đánh nhau
        {
            while (enemyAlive > 0)
            {
                yield return null;
            }
            for (int i = 0; i < levelDatas[currentLevel].enemiesPerWave; i++)
            {
                RandomSpawnEnemy();
                yield return new WaitForSeconds(1); // delay between spawns
            }
            //  cập nhật số lượng enemy sống ở scene
            enemyAlive += levelDatas[currentLevel].enemiesPerWave;
            countWaves++;
            yield return new WaitForSeconds(2); // delay between waves
        }
    }
    void RandomSpawnEnemy()
    {
        int len = levelDatas[currentLevel].enemyPrefab.Count; // số lượng kiểu enemy spawn ở màn này
        Vector2 spawnPos = new Vector3(Random.Range(-50,50), 0);
        Instantiate(levelDatas[currentLevel].enemyPrefab[Random.Range(0, len)], spawnPos, Quaternion.identity);
    }

}