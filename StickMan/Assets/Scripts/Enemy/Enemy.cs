using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int HP;
    protected int speed;

    public void SetHP(int hp) // Phương thức để thiết lập HP
    {
        HP = hp;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
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

    public abstract void Move();
    public abstract void Attack();
}