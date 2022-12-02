using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : CharacterAliveState
{
    public CharacterDeadState( CharacterBaseStateMachine stateMachine) : base("Dead", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        sm.anim.SetBool("Dead", true);

    }


    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("Dead", false);
    }
}
