using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanAttackableState : CharacterCanSkillCastableState
{
    public bool stillPressingAttack = false;

    public CharacterCanAttackableState(string name, CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        stillPressingAttack = false;
    }
    public override void Update()
    {
        base.Update();

        Attacking();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        
    }

    private void Attacking()
    {
        //UI Input gelince degisecek!!
        if (Input.GetKey(KeyCode.Space) && !stillPressingAttack)
        {
            stillPressingAttack = true;
            Debug.Log("keypress");

            sm.ChangeState(sm.characterAttackingState);


        }
        if (UIManager.Instance.isAttackPress&&!stillPressingAttack)
        {
            stillPressingAttack = true;
            sm.ChangeState(sm.characterAttackingState);
        }
       


    }
  
}
