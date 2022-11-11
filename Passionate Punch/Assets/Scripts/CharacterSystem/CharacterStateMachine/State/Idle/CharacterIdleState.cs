using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterCanAttackableState
{

   

    public CharacterIdleState(CharacterBaseStateMachine stateMachine) : base("Idle", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        sm.ChangeState(sm.characterMovingState);
    }
    public override void Update()
    {
        base.Update();
        //CheckMovementInput();
    }
    public override void Exit()
    {
        base.Exit();

    }




    public void CheckMovementInput()
    {
        float xInput = 0;
        float zInput = 0;
        
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                xInput = UIManager.Instance.joystickHorizontalInput;
                zInput = UIManager.Instance.joystickVerticalInput;
                break;

            case RuntimePlatform.WindowsEditor:
                xInput = Input.GetAxis("Horizontal");
                zInput = Input.GetAxis("Vertical");
                break;
        }
      
        if (Mathf.Abs(xInput) > 0.4f || Mathf.Abs(zInput) > 0.4f)
        {
            sm.ChangeState(sm.characterMovingState);
        }
    }

    IEnumerator WaitForFrame(float time)
    {
        yield return new WaitForSecondsRealtime(time);
       
    }

   
}
