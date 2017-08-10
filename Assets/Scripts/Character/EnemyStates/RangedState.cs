using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

    private Enemy enemy;

    private float fireTimer;
    private float fireCoolDown = 3f;
    private bool canFire = true;


    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
        
    {

        FireBullet();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else {
            enemy.ChangeState(new IdleState());
        }
       
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void FireBullet() {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCoolDown)
        {
            canFire = true;
            fireTimer = 0;
        }

        if (canFire) {
            canFire = false;
            enemy.MyAnimator.SetTrigger("Fire");
        }
    }

}
