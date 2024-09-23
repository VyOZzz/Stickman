using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
   protected int damage;
   protected float cooldownTime;
   protected bool canAttack = true;
   // sử dụng IEnumerator để cooldown
   protected IEnumerator AttackCooldown()
   {
      canAttack = false;
      yield return new WaitForSeconds(cooldownTime);
      canAttack = true;
   }
   public abstract void Attack();
   public void SetDamage(int newDamane)
   {
      damage = newDamane;
   }
}
