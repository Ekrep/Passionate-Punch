using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSM : EnemyStateMachine
{
    public ScriptableFloat enemyMovementSpeed, enemyAttackDistance;
    public Transform enemyCampPos;
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
    private void Awake()
    {
        enemy = this.gameObject;
        enemyIdleState = new EnemyIdleState(this);
        enemyMovingState = new EnemyMovingState(this);
        enemyChasingState = new EnemyChasingState(this);
        enemyReturnState = new EnemyReturnState(this);
        enemyAttackState = new EnemyAttackState(this);
    }
    protected override EnemyBaseState GetInitialState()
    {
        return enemyIdleState;
    }
}
