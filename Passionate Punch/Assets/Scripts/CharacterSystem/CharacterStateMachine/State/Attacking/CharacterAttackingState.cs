using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterAliveState
{


    public CharacterAttackingState(CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        sm.anim.SetBool("Attack", true);
        Debug.Log("enabled");
        sm.testObject.SetActive(true);
      



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
            
            sm.testObject.SetActive(false);
        }

    }


    public override void Exit()
    {

        base.Exit();


        Debug.Log("exx");
        sm.anim.SetBool("Attack", false);
      
        sm.StopCoroutine(WaitForFrame(0.5f));
        



    }

  

    IEnumerator WaitForFrame(float timeX)
    {
        yield return new WaitForSecondsRealtime(timeX);
        sm.ChangeState(sm.characterIdleState);
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
