using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSM : EnemyStateMachine
{
    [HideInInspector]
    public EnemyIdleState enemyIdleState;
    [HideInInspector]
    public EnemyMovingState enemyMovingState;
    private void Awake()
    {
        enemyIdleState = new EnemyIdleState(this);
        enemyMovingState = new EnemyMovingState(this);
    }
    protected override EnemyBaseState GetInitialState()
    {
        return enemyIdleState;
    }
}
