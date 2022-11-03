using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterCanAttackableState
{
    

    private bool _isAttacking;
    public CharacterAttackingState(CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        sm.anim.SetBool("Attack", true);
       


    }
    public override void Update()
    {
        base.Update();


    }


    public override void Exit()
    {

        base.Exit();
        
     
        Debug.Log("exx");
        sm.anim.SetBool("Attack", false);
       



    }

 







}
