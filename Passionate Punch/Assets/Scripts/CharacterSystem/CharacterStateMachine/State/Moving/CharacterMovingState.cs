using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingState : CharacterAliveState
{
    public CharacterMovingState( CharacterBaseStateMachine stateMachine) : base("Moving", stateMachine)
    {

    }
}
