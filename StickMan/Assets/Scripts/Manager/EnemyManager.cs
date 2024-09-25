using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject skeletonPrefab;
    
    public void SpawnSlime(Vector3 position)
    {
        Instantiate(slimePrefab, position, Quaternion.identity);
        
    }
    public void SpawnSkeleton(Vector3 position)
    {
        Instantiate(skeletonPrefab, position, Quaternion.identity);
        
    }
    void Start()
    {
        //SpawnSlime(new Vector2(0, 0));       // Spawn Slime ở vị trí 0,0
        SpawnSkeleton(new Vector2(-5, 0));    // Spawn Skeleton ở vị trí (5,0)
    }
}
