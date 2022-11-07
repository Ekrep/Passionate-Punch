using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

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
       
      



    }
    public override void Update()
    {
        base.Update();
        SetRotationWhileAttacking();
       /* if (Input.GetKeyUp(KeyCode.Space))
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
        if (!UIManager.Instance.isAttackPress)
        {
            if (CheckMovementInput())
            {
                sm.ChangeState(sm.characterMovingState);
            }
            else
            {
                sm.ChangeState(sm.characterIdleState);
            }


        }*/


    }

    public void ChangeAttackState()
    {
        if (!UIManager.Instance.isAttackPress)
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

    public void Attack()
    {
        RaycastHit[] raycastHits=new RaycastHit[1];
        Physics.RaycastNonAlloc(sm.transform.position, sm.transform.forward, raycastHits, sm.characterStats.range);
        Debug.DrawRay(sm.transform.position, sm.transform.forward, Color.red, 20);
        if (raycastHits[0].collider != null)
        {
            
            Collider[] colliders=new Collider[50];
            int count=0;
            
                Debug.Log(raycastHits[0].collider.gameObject.name);
            
            
            count=Physics.OverlapSphereNonAlloc(raycastHits[0].point, sm.characterStats.AEORange, colliders);
            for (int i = 0; i <count ; i++)
            {
                if (colliders[i].gameObject.TryGetComponent<Chest>(out Chest chest))
                {
                    Debug.Log("collision");
                }
                
            }
            
            
        }
     
    }
    
    public override void Exit()
    {

        base.Exit();


        Debug.Log("exx");
        sm.anim.SetBool("Attack", false);
       

    }

 

   public void SetRotationWhileAttacking()
    {
        //float xInput = UIManager.Instance.joystickHorizontalInput;
        //float zInput = UIManager.Instance.joystickVerticalInput;
        float xInput = 0;
        float zInput = 0;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        sm.gameObject.transform.rotation = Quaternion.Euler(sm.gameObject.transform.rotation.x, sm.gameObject.transform.rotation.y+(xInput*10), sm.gameObject.transform.rotation.z);
        
        
        

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
