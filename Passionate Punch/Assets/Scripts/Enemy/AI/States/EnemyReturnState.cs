using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject enemy;
    Vector3 enemyPos;
    float timeToStartReturning, returnSpeed, distanceToTarget, isReached;
    public EnemyReturnState(EnemyMovementSM enemyStateMachine) : base("Return", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Returning state");
        isReached = 0.1f;
        timeToStartReturning = 1f;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
        returnSpeed = enemyMovementSM.enemyMovementSpeed.value * Time.deltaTime; // May decrease the value in the future
        enemyMovementSM.enemyCanvasAnimator.SetTrigger("QuitWarning");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        timeToStartReturning -= Time.deltaTime;
        if (timeToStartReturning <= 0)
        {
            ReturnToCamp();
        }
    }
    public override void EnemyTriggerEnter(Collider other)
    {
        base.EnemyTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyMovementSM.enemyCanvasAnimator.SetTrigger("EnterWarning");
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Returning state");
    }
    // TO force enemy to return to its camp
    void ReturnToCamp()
    {
        enemyPos = enemy.transform.position;
        //enemy.transform.position = Vector3.MoveTowards(enemyPos, enemyMovementSM.enemyCampPos.position, returnSpeed);
        enemyMovementSM.enemyNavMesh.SetDestination(enemyMovementSM.enemyCampPos.position);
        distanceToTarget = Vector3.Distance(enemyPos, enemyMovementSM.enemyCampPos.position);
        if (distanceToTarget <= isReached && !enemyMovementSM.isPatrollingEnemy)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyIdleState);
        }
        else if (distanceToTarget <= isReached && enemyMovementSM.isPatrollingEnemy)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyPatrollingState);
        }
    }
}
