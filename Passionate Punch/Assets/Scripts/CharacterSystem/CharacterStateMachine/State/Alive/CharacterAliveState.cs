using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAliveState : CharacterBaseState
{
    protected CharacterBaseStateMachine sm;




    public CharacterAliveState(string name, CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {
        sm = (CharacterBaseStateMachine)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter");
        
    }
    









}
