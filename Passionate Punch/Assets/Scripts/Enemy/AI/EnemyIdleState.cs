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
            Debug.Log("State has been changed to ==> Moving");
            enemyStateMachine.ChangeState(enemyMovementSM.enemyMovingState);
        }
    }
}
