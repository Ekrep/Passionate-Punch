using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSM : EnemyStateMachine
{
    public ScriptableFloat enemyMovementSpeed, enemyReturningSpeed, enemyPatrollingSpeed, enemyAttackDistance;
    public Transform enemyCampPos;
    public List<Transform> patrolPositions;
    public GameObject stunParticles, warnEnemy;
    public Animator enemyCanvasAnimator;
    public Animator enemyAnimator;
    int empty = 0;
    [HideInInspector]
    public bool isPatrollingEnemy;
    [HideInInspector]
    public NavMeshAgent enemyNavMesh;
    // Our enemy game object
    [HideInInspector]
    public GameObject enemy;
    ////////////////////////
    [HideInInspector]
    public EnemyIdleState enemyIdleState;
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
        enemyNavMesh = GetComponent<NavMeshAgent>();
        // Set pasive the stun particles & Warn enemy canvas
        stunParticles.gameObject.SetActive(false);
        warnEnemy.gameObject.SetActive(false);
        ////////////////////////////////////////////////////
        enemy = this.gameObject;
        enemyIdleState = new EnemyIdleState(this);
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
