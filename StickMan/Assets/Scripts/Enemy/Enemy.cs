using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Enemy : MonoBehaviour
{
    public int HP;
    
    private void Start()
    {
    }

    public void SetHP(int hp) // Phương thức để thiết lập HP
    {
        HP = hp;
    }
    // public vì có thể có nhiều cái khác gây sát thương ra k chỉ enemy
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die(); // Gọi phương thức để xử lý khi chết
        }
    }
    protected virtual void Die()
    {
        // Logic cho cái chết, như hủy đối tượng hoặc phát hoạt ảnh
        Destroy(gameObject);
    }
}