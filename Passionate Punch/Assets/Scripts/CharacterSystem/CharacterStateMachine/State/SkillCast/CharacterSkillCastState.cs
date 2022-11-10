using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillCastState : CharacterAliveState
{
    public CharacterSkillCastState( CharacterBaseStateMachine stateMachine) : base("SkillCast", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        sm.anim.SetBool("SkillCasting", true);
    }

    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("SkillCasting", false);

    }
}
