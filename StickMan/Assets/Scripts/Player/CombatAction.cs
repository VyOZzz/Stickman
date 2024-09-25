using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAction : MonoBehaviour
{
   protected int damage = 10;
   protected float cooldownTime;
   protected bool canAttack = true;

   protected bool isAttack;
   // sử dụng IEnumerator để cooldown
   protected IEnumerator AttackCooldown()
   {
      canAttack = false;
      yield return new WaitForSeconds(cooldownTime);
      canAttack = true;
   }
   // cần public vì có thể các script xử lý input có thể sẽ cần gọi attack nên cần public
   public abstract void Attack();
   // cần public để setdamage khi muốn nhặt vật phẩm hay gì đó.
   public void SetDamage(int newDamane)
   {
      damage = newDamane;
   }
   
}
