using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Manager;
using Player;
using UnityEngine;

public abstract class CombatAction : MonoBehaviour
{
   [SerializeField] protected int damage = 10;
   protected float cooldownTime = 1f;
   protected Animator animator;
   private void Start()
   {
      animator = GetComponent<Animator>();
   }

   // sử dụng IEnumerator để cooldown
   // protected virtual IEnumerator AttackCooldown()
   // {
   //    canAttack = false;
   //    animator.SetBool(AnimationStrings.canAttack, false);
   //    yield return new WaitForSeconds(cooldownTime);
   //    canAttack = true;
   //    animator.SetBool(AnimationStrings.canAttack, true);
   // }
   // cần public vì có thể các script xử lý input có thể sẽ cần gọi attack nên cần public
   public abstract void Attack();
   // cần public để setdamage khi muốn nhặt vật phẩm hay gì đó.
   public void SetDamage(int newDamage)
   {
      damage = newDamage;
   }
   
}
