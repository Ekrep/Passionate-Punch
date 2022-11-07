using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    public EnemyIdleState(EnemyMovementSM enemyStateMachine) : base("Idle", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Idle State");
        // When enemy enters the Idle state, the warning canvas will be passive
        enemyMovementSM.warnEnemy.gameObject.SetActive(false);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
    // Collision Enter on Idle State
    public override void EnemyOnCollisionEnter(Collision collision)
    {
        base.EnemyOnCollisionEnter(collision);
    }
    // IF the player enters the trigger of the enemy, enemy starts to chasing him. (After some preparation time)
    public override void EnemyTriggerEnter(Collider other)
    {
        base.EnemyTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyMovementSM.warnEnemy.gameObject.SetActive(true);
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Idle State");
    }
}
