using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterAliveState
{

    private float _timer=0.5f;
    private float _timerflag;

    public CharacterAttackingState(CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _timerflag = _timer;
        sm.anim.SetBool("Attack", true);
        Debug.Log("enabled");
      
      



    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (CheckMovementInput())
            {
                sm.ChangeState(sm.characterMovingState);
            }
            else
            {
                sm.ChangeState(sm.characterIdleState);
            }

            
        }
        

    }


    public override void Exit()
    {

        base.Exit();


        Debug.Log("exx");
        sm.anim.SetBool("Attack", false);
       





    }

 

   



    public bool CheckMovementInput()
    {
        float xInput = 0;
        float zInput = 0;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {
            sm.ChangeState(sm.characterMovingState);
            return true;
        }
        else
        {
            return false;
        }
    }





}
