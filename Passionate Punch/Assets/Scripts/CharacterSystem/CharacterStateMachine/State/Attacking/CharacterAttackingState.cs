using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterAliveState
{
    public CharacterAttackingState(string name, CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }
}
