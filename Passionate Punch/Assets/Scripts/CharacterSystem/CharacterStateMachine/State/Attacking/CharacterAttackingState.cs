using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterAliveState
{
    public CharacterAttackingState( CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }
}
