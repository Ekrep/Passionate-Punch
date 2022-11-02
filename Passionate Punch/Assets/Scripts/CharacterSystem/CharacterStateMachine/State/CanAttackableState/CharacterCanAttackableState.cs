using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanAttackableState : CharacterAliveState
{
    public CharacterCanAttackableState(string name,CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {

    }

    public override void Update()
    {
        base.Update();
        Attacking();
    }

    private void Attacking()
    {
        //UI Input gelince degisecek!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.ChangeState(sm.characterAttackingState);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            sm.ChangeState(sm.characterIdleState);
        }
    }

}
