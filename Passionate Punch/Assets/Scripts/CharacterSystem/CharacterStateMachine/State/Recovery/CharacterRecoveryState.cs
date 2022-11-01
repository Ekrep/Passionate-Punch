using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecoveryState : CharacterAliveState
{
    public CharacterRecoveryState( CharacterBaseStateMachine stateMachine) : base("Recovery", stateMachine)
    {

    }
}
