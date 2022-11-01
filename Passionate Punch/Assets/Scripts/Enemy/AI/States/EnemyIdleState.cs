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
    public override void Exit()
    {
        base.Exit();
        Debug.Log("State has been changed to ==> Moving");
    }
}
