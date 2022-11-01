using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    public EnemyMovingState(EnemyMovementSM enemyStateMachine) : base("Moving", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Moving State");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked On Moving State");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyIdleState);
        }
    }
    // Collision Enter on Moving State
    public override void EnemyOnCollisionEnter(Collision collision)
    {
        base.EnemyOnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player Occured on Moving State");
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("State has been changed to ==> Idle");
    }
}