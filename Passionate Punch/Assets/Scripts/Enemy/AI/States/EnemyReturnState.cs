using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject enemy;
    Vector3 enemyPos;
    float timeToStartReturning, returnSpeed, distanceToTarget, isReached, stopDistance;
    public EnemyReturnState(EnemyMovementSM enemyStateMachine) : base("Return", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Returning state");
        enemyMovementSM.enemyAnimator.SetTrigger("Idle");
        // Enemy stop distance between camp pos & enemy rearranged
        stopDistance = .05f;
        enemyMovementSM.enemyNavMesh.stoppingDistance = stopDistance;
        //////////////////////////////////////////////////////////
        isReached = 0.1f;
        timeToStartReturning = 1f;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
        // Setting the returnin speed of the enemy
        returnSpeed = enemyMovementSM.enemyReturningSpeed.value; // May decrease the value in the future
        enemyMovementSM.enemyNavMesh.speed = returnSpeed;
        ////////////////////////////////////////////////////////
        enemyMovementSM.enemyCanvasAnimator.SetTrigger("QuitWarning");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.warnEnemy.transform.LookAt(Camera.main.transform);
        timeToStartReturning -= Time.deltaTime;
        if (timeToStartReturning <= 0)
        {
            enemyMovementSM.enemyAnimator.ResetTrigger("Idle");
            ReturnToCamp();
            enemyMovementSM.enemyAnimator.SetTrigger("Walk");
        }
        else
        {
            // The time before enemy turn back
            // This Idle will be changed with "lost player" animation 
            enemyMovementSM.enemyAnimator.SetTrigger("Idle");
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
        enemyMovementSM.enemyAnimator.ResetTrigger("Walk");
        enemyMovementSM.enemyAnimator.ResetTrigger("Idle");
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
