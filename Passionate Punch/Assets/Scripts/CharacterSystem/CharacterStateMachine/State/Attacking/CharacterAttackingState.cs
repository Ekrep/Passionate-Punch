using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CharacterAttackingState : CharacterAliveState
{

    private bool _stuckItHere;

    private float _stuckTime;

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
        ControlStuck();
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
        
        if (!UIManager.Instance.isAttackPress&&!_stuckItHere)
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
    private void ControlStuck()
    {
        Debug.Log(_stuckTime);
        if (UIManager.Instance.isAttackPress)
        {
            _stuckItHere = true;
            _stuckTime = 0.7f;
        }
        else
        {
            
            if (_stuckTime<=0)
            {
                _stuckItHere = false;
            }
            else
            {
                _stuckTime -= Time.deltaTime * sm.anim.GetFloat("AttackSpeed"); ;
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
        /*if (Mathf.Abs(xInput)>0|| Mathf.Abs(zInput) > 0)
        {
            sm.gameObject.transform.eulerAngles += new Vector3(sm.gameObject.transform.eulerAngles.x, sm.gameObject.transform.eulerAngles.y + (xInput+zInput)*5, sm.gameObject.transform.eulerAngles.z) * Time.deltaTime;
        }*/

        Debug.Log(xInput+"x");
        Debug.Log(zInput+"z");
        
        

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
