using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState
{
    public string name;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyBaseState(string name, EnemyStateMachine enemyStateMachine)
    {
        this.name = name;
        this.enemyStateMachine = enemyStateMachine;
    }
    public virtual void Enter(){}
    public virtual void UpdateLogic(){}
    public virtual void UpdatePhysics(){}
    public virtual void Exit(){}
}
