using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected bool canAttack;
    protected float CooldownTime;
    [SerializeField]protected DamageHandle damageHandle;

    private void Awake()  // Đổi từ Start sang Awake
    {
        damageHandle = GetComponentInChildren<DamageHandle>();
    }
    protected IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(CooldownTime);
        canAttack = true;
    }
    public abstract void Move();
    public abstract void Attack();
    public abstract void ReceivedDamage();
    public  void  SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
}
