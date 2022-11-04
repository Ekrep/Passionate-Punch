using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject enemy;
    Vector3 enemyCurrentPos, targetPos;
    int currentPatrolPosIndex, lastIndex;
    float patrolMoveSpeed, distance, isCloseEnough;
    bool isPatrolling;
    public EnemyPatrollingState(EnemyMovementSM enemyStateMachine) : base("Patrolling", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the Patrolling State");
        // 1.5 unit per frame
        patrolMoveSpeed = 1.5f * Time.deltaTime;
        // Enemy object
        enemy = enemyMovementSM.enemy.gameObject;
        // Our Starting position of enemy
        enemyCurrentPos = enemy.transform.position;
        // Our index of the patrol points
        currentPatrolPosIndex = 0;
        isPatrolling = false;
        isCloseEnough = .1f;
        // Last Index of patrolling positions
        lastIndex = enemyMovementSM.patrolPositions.Count;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyCurrentPos = enemyMovementSM.enemy.transform.position;
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
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Patrolling State");
    }
    void PatrolBetweenPoints()
    {
        enemy.transform.position = Vector3.MoveTowards(enemyCurrentPos, targetPos, patrolMoveSpeed);
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
