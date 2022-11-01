using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterAliveState
{
    

    public CharacterIdleState(string name,CharacterBaseStateMachine stateMachine) : base("Idle", stateMachine)
    {

    }
}
