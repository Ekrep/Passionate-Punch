using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
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
        Debug.Log("Entered Attack State");
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        CalculateDistanceAndAttack();
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Attack State");
    }
    void CalculateDistanceAndAttack()
    {
        distance = Vector3.Distance(enemyPos, playerPos);
        if (distance > enemyMovementSM.enemyAttackDistance.value)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
        else if (distance <= enemyMovementSM.enemyAttackDistance.value)
        {
            // Attack to the player
        }
    }
}