using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState         
{
    private EnemyMovementSM enemyMovementSM;
    public static Action OnEnemyDie;
    float cleanUpTime;
    public EnemyDieState(EnemyMovementSM enemyStateMachine) : base("Die", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        OnEnemyDie?.Invoke();
        cleanUpTime = 3f;
        enemyMovementSM.enemyAnimator.SetTrigger("Die");
        enemyMovementSM.warnEnemy.gameObject.SetActive(false);
        enemyMovementSM.enemy.GetComponent<Collider>().enabled = false;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.focusCanvas.gameObject.SetActive(false);
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
    }
    void CleanUpBody()
    {
        enemyMovementSM.enemy.gameObject.SetActive(false);
        enemyMovementSM.enemy.GetComponent<Collider>().enabled = true;
    }
}
