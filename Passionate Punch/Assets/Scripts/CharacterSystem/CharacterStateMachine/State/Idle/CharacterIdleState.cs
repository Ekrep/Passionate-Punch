using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterAliveState
{

   

    public CharacterIdleState(CharacterBaseStateMachine stateMachine) : base("Idle", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        base.Update();
        CheckMovementInput();
    }
    public override void Exit()
    {
        base.Exit();

    }




    public void CheckMovementInput()
    {
        float xInput = 0;
        float zInput = 0;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {
            sm.ChangeState(sm.characterMovingState);
        }
    }

   
}
