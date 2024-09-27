using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private int HP = 100;
    [SerializeField] private Heathbar heathBar;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Animator animator;
    public int GetHP
    {
        get
        {
            return HP;
        }
    }
    private void Start()
    {
        heathBar.SetMaxHeath(HP);
        animator = GetComponentInParent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(HP);
        HP -= damage;
        heathBar.SetHeath(HP);
        if (HP <= 0)
            Die();
    }
    private void Die()
    {
        
        StartCoroutine(DieDelay());
    }
    IEnumerator DieDelay()
    {
        animator.SetBool(AnimationStrings.isDeath, true);
        yield return new WaitForSeconds(1);
        Destroy(playerCtrl.gameObject);
        levelManager.GameOver();
    }
}
