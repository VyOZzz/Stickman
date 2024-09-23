using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandle : MonoBehaviour
{
    [SerializeField] private int HP;

    public int Hp
    {
        get => HP;
        set => HP = value;
    }
    public void SetHP(int newHP)
    {
        HP = newHP;
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    public void DieCheck()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
