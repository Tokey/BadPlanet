using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private float patrolTimer;
    public float patrolDuration=5f;

    private Enemy enemy;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
       // Debug.Log("Enemy Patrolling");
        Patrol();

        enemy.Move();

        if (enemy.Target != null && enemy.InFireRange) {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
       // if (other.tag == "Edge") {

       //     enemy.ChangeDirection();
      //  }

        if (other.tag == "Bullet")
        {
            enemy.Target = Player.Instance.gameObject;
        }

    }

    private void Patrol()
    {
        
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
