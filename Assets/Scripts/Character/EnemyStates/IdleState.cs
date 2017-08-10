using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{

    private Enemy enemy;
    private float idleTimer;

    public float idleDuration=1f;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        //Debug.Log("Enemy Idling");
        Idle();

        if (enemy.Target != null) {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Bullet") {
            enemy.Target = Player.Instance.gameObject;
        }   
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("Speed", 0);
        idleTimer += Time.deltaTime;
        if(idleTimer>=idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

}
