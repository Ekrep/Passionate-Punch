using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanAttackableState : CharacterAliveState
{
    public bool stillPressingAttack=false;
   
    public CharacterCanAttackableState(string name,CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {

    }
   
    public override void Update()
    {
        base.Update();
       
        
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        Attacking();
    }

    private void Attacking()
    {
        //UI Input gelince degisecek!!
        if (Input.GetKey(KeyCode.Space)&&!stillPressingAttack)
        {
            stillPressingAttack = true;
            Debug.Log("keypress");
          
            sm.ChangeState(sm.characterAttackingState);
            
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            
           stillPressingAttack = false;
            sm.ChangeState(sm.characterIdleState);
        }
       

    }

}
