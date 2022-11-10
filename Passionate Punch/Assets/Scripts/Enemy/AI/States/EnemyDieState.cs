using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState         
{
    private EnemyMovementSM enemyMovementSM;
    float cleanUpTime;
    public EnemyDieState(EnemyMovementSM enemyStateMachine) : base("Die", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Die State");
        cleanUpTime = 3f;
        enemyMovementSM.enemyAnimator.SetTrigger("Die");
        enemyMovementSM.warnEnemy.gameObject.SetActive(false);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        cleanUpTime -= Time.deltaTime;
        // If clean up time reached, enemy body will cleaned up
        if (cleanUpTime <= 0)
        {
            CleanUpBody();
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Die State");
    }
    void CleanUpBody()
    {
        enemyMovementSM.enemy.gameObject.SetActive(false);
    }
}
