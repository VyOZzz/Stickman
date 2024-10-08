using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<GameObject> enemyPrefab; // level1 just have skeleton and mushroom, level2 still like level1
    // lvel 3 have gobin +level2
    public int totalEnemies;
    public int enemiesPerWave;
    public int numberWave;// number of waves
}
