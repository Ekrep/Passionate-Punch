using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : CharacterAliveState
{
    public CharacterDeadState( CharacterBaseStateMachine stateMachine) : base("Dead", stateMachine)
    {

    }
}
