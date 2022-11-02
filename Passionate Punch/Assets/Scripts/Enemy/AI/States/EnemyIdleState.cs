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
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked On Idle State");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyMovingState);
        }
    }
    // Collision Enter on Idle State
    public override void EnemyOnCollisionEnter(Collision collision)
    {
        base.EnemyOnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player Occured On Idle State");
        }
    }
    // IF the player enters the trigger of the enemy, enemy starts to chasing him. (After some preparation time)
    public override void EnemyTriggerEnter(Collider other)
    {
        base.EnemyTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Idle State");
    }
}
