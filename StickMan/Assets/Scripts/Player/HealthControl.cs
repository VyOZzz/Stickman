using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private int HP = 100;

    public void TakeDamage(int damage)
    {
        Debug.Log(HP);
        HP -= damage;
        if (HP <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(playerCtrl.gameObject);
    }
}
