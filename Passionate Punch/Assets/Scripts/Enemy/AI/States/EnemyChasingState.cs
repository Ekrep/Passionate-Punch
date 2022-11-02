using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    public EnemyChasingState(EnemyMovementSM enemyStateMachine) : base("Chasing", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the chasing state");
    }
    // IF player exits from the enemy's trigger, enemy returns to its idle state
    public override void EnemyTriggerExit(Collider other)
    {
        base.EnemyTriggerExit(other);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Chasing State");
    }
}
