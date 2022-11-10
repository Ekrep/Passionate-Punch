using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject enemy;
    Vector3 enemyCurrentPos, targetPos;
    int currentPatrolPosIndex, lastIndex;
    float patrolMoveSpeed, distance, isCloseEnough, stopDistance;
    bool isPatrolling;
    public EnemyPatrollingState(EnemyMovementSM enemyStateMachine) : base("Patrolling", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Patrolling State");
        enemyMovementSM.enemyAnimator.SetTrigger("Walk");
        // 1.5 unit per frame
        patrolMoveSpeed = enemyMovementSM.enemyPatrollingSpeed.value;
        enemyMovementSM.enemyNavMesh.speed = patrolMoveSpeed;
        // Stop distance between patrolling points
        isCloseEnough = .05f;
        enemyMovementSM.enemyNavMesh.stoppingDistance = isCloseEnough;
        // Stop distance between player and enemy
        stopDistance = 2f;
        /////////////////////////////////////////
        // Enemy object
        enemy = enemyMovementSM.enemy.gameObject;
        // Our Starting position of enemy
        enemyCurrentPos = enemy.transform.position;
        // Our index of the patrol points
        currentPatrolPosIndex = 0;
        isPatrolling = false;
        // Last Index of patrolling positions
        lastIndex = enemyMovementSM.patrolPositions.Count;
        // When enemy enters the Idle state, the warning canvas will be passive
        enemyMovementSM.warnEnemy.gameObject.SetActive(false);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyCurrentPos = enemyMovementSM.enemy.transform.position;
        enemyMovementSM.enemyAnimator.SetTrigger("Walk");
        if (isPatrolling)
        {
            PatrolBetweenPoints();
            ArriveControl();
        }
        else
        {
            ArrangePatrolIndex();
        }
    }
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
        Debug.Log("Exit Patrolling State");
        enemyMovementSM.enemyNavMesh.stoppingDistance = stopDistance;
        enemyMovementSM.enemyAnimator.ResetTrigger("Walk");
    }
    void PatrolBetweenPoints()
    {
        //enemy.transform.position = Vector3.MoveTowards(enemyCurrentPos, targetPos, patrolMoveSpeed); (OLD SYSTEM)
        enemyMovementSM.enemyNavMesh.SetDestination(targetPos);
    }
    void ArriveControl()
    {
        distance = Vector3.Distance(enemyCurrentPos, targetPos);
        if (distance <= isCloseEnough)
        {
            isPatrolling = false;
        }
    }
    void ArrangePatrolIndex()
    {
        currentPatrolPosIndex += 1;
        if (currentPatrolPosIndex == lastIndex)
        {
            // Turn index to beginning of the list
            currentPatrolPosIndex = 0;
        }
        targetPos = enemyMovementSM.patrolPositions[currentPatrolPosIndex].position;
        distance = Vector3.Distance(enemyCurrentPos, targetPos);
        isPatrolling = true;
    }
}
