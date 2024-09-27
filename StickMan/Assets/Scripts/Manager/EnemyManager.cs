using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject mushroom;
    public int countEnemy;
    public void SpawnEnemies()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
        {
            SpawnEnemy(mushroom, 5);
            countEnemy = 5;

        }else if (currentSceneIndex == 2)
        {
            SpawnEnemy(mushroom, 5);
            SpawnEnemy(skeletonPrefab, 5);
            countEnemy = 10;
        }
    }
    void SpawnEnemy(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = new Vector3(Random.Range(-40,40), 0);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
