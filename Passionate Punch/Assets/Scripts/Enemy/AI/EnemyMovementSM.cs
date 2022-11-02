using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSM : EnemyStateMachine
{
    [HideInInspector]
    public EnemyIdleState enemyIdleState;
    [HideInInspector]
    public EnemyMovingState enemyMovingState;
    [HideInInspector]
    public EnemyChasingState enemyChasingState;
    private void Awake()
    {
        enemyIdleState = new EnemyIdleState(this);
        enemyMovingState = new EnemyMovingState(this);
        enemyChasingState = new EnemyChasingState(this);
    }
    protected override EnemyBaseState GetInitialState()
    {
        return enemyIdleState;
    }
}
