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
        Attack();
      



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
        RaycastHit[] raycastHits=new RaycastHit[0];
        Physics.RaycastNonAlloc(sm.transform.position, sm.transform.forward, raycastHits, sm.characterStats.range);
        Debug.DrawRay(sm.transform.position, sm.transform.forward, Color.red, 20);
        if (raycastHits.Length!=0)
        {
            Collider[] colliders=new Collider[50];
            int count=0;
            Debug.Log(raycastHits[0].collider.gameObject.name);
            count=Physics.OverlapSphereNonAlloc(raycastHits[0].point, 50, colliders);
            for (int i = 0; i <count ; i++)
            {
                Debug.Log(colliders[i].gameObject.name);
                
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
