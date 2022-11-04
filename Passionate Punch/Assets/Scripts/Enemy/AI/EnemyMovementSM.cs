using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSM : EnemyStateMachine
{
    public ScriptableFloat enemyMovementSpeed, enemyAttackDistance;
    public Transform enemyCampPos;
    public List<Transform> patrolPositions;
    public GameObject stunParticles;
    int empty = 0;
    [HideInInspector]
    public bool isPatrollingEnemy;
    // Our enemy game object
    [HideInInspector]
    public GameObject enemy;
    ////////////////////////
    [HideInInspector]
    public EnemyIdleState enemyIdleState;
    [HideInInspector]
    public EnemyMovingState enemyMovingState;
    [HideInInspector]
    public EnemyChasingState enemyChasingState;
    [HideInInspector]
    public EnemyReturnState enemyReturnState;
    [HideInInspector]
    public EnemyAttackState enemyAttackState;
    [HideInInspector]
    public EnemyStunState enemyStunState;
    [HideInInspector]
    public EnemyPatrollingState enemyPatrollingState;
    private void Awake()
    {
        // Set pasive the stun particles
        stunParticles.gameObject.SetActive(false);
        enemy = this.gameObject;
        enemyIdleState = new EnemyIdleState(this);
        enemyMovingState = new EnemyMovingState(this);
        enemyChasingState = new EnemyChasingState(this);
        enemyReturnState = new EnemyReturnState(this);
        enemyAttackState = new EnemyAttackState(this);
        enemyStunState = new EnemyStunState(this);
        enemyPatrollingState = new EnemyPatrollingState(this);
    }
    protected override EnemyBaseState GetInitialState()
    {
        if (patrolPositions.Count > empty)
        {
            isPatrollingEnemy = true;
            return enemyPatrollingState;
        }
        else
        {
            isPatrollingEnemy = false;
            return enemyIdleState;
        }
    }
}
