using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Die();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(HP);
        HP -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
