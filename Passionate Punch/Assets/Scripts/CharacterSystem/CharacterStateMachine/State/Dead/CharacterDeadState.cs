using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : CharacterAliveState
{
    public CharacterDeadState(string name, CharacterBaseStateMachine stateMachine) : base("Dead", stateMachine)
    {

    }
}
