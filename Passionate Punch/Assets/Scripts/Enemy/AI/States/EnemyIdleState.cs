using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    float stopDistance;
    public EnemyIdleState(EnemyMovementSM enemyStateMachine) : base("Idle", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        enemyMovementSM.enemyAnimator.SetTrigger("Idle");
        stopDistance = 2f;
        // When enemy enters the Idle state, the warning canvas will be passive
        enemyMovementSM.warnEnemy.gameObject.SetActive(false);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.enemyAnimator.SetTrigger("Idle");
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
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<CharacterBaseStateMachine>().canVisible)
        {
            enemyMovementSM.warnEnemy.gameObject.SetActive(true);
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void EnemyTriggerStay(Collider other)
    {
        base.EnemyTriggerStay(other);
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<CharacterBaseStateMachine>().canVisible)
        {
            enemyMovementSM.warnEnemy.gameObject.SetActive(true);
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemyMovementSM.enemyNavMesh.stoppingDistance = stopDistance;
        enemyMovementSM.enemyAnimator.ResetTrigger("Idle");
    }
}
