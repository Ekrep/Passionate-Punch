using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
public class EnemyAttackState : EnemyBaseState
{
    public static Action<float> OnPlayerTakeDamage;
    // Player takes hit in time
    float hitPlayerTime; 
    private EnemyMovementSM enemyMovementSM;
    float distance;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    public EnemyAttackState(EnemyMovementSM enemyStateMachine) : base("Attack", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        hitPlayerTime = 0;
        enemyMovementSM.enemyAnimator.SetTrigger("Attack");
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();  
        enemyMovementSM.enemyAnimator.SetTrigger("Attack");
        // Enemy's warning UI must always looks at to the camera
        enemyMovementSM.warnEnemy.transform.LookAt(Camera.main.transform);
        // Update player's and enemy's positions in every frame
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        // Enemy attacks to the player with respect to distance.
        CalculateDistanceAndAttack();
    }
    public override void Exit()
    {
        base.Exit();
        enemyMovementSM.enemyAnimator.ResetTrigger("Attack");
    }
    void CalculateDistanceAndAttack()
    {
        hitPlayerTime -= Time.deltaTime;
        distance = Vector3.Distance(enemyPos, playerPos);
        if (distance > enemyMovementSM.enemyAttackDistance.value)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
        else if (distance <= enemyMovementSM.enemyAttackDistance.value)
        {
            // Attack to the player
            enemyMovementSM.enemy.transform.LookAt(player.transform);
            if (hitPlayerTime <= 0)
            {
                player.GetComponent<CharacterHealth>().Hit(SkillSystem.SkillSettings.HitType.Low, 5, enemyPos, 1); // 5 will be attack damage of the enemy
                hitPlayerTime = 2f; // Attack Animation's total time (2f)
            }
        }
    }
}
