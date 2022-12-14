using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingState : CharacterCanAttackableState
{

    
    public CharacterMovingState( CharacterBaseStateMachine stateMachine) : base("Moving", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
       
        
    }

   
    public override void Update()
    {
        base.Update();
       
      
        Move();




    }
    public override void LateUpdate()
    {
        base.LateUpdate();
       
        
       



    }
    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("Moving", false);
       

    }


    private void Move()
    {
        float xInput=0;
        float zInput=0;
       
        
        switch (UnityEngine.Device.Application.platform)
        {
            case RuntimePlatform.Android:
                xInput = UIManager.Instance.joystickHorizontalInput;
                zInput = UIManager.Instance.joystickVerticalInput;
                break;

            case RuntimePlatform.WindowsEditor:
                xInput = Input.GetAxisRaw("Horizontal");
                zInput = Input.GetAxisRaw("Vertical");
                break;

                
        }
        if (Mathf.Abs(xInput) < 0.4f &&Mathf.Abs(zInput) < 0.4f)
        {
            
            sm.anim.SetBool("Moving", false);
        }
        //UI Input geldiginde degisecek
        if (Mathf.Abs(xInput) > 0.4f || Mathf.Abs(zInput) > 0.4f)
        {
            sm.anim.SetBool("Moving", true);
            Vector3 moveDir=new Vector3(xInput,0,zInput).normalized;
            
            sm.transform.position +=moveDir*sm.characterStats.moveSpeed*Time.deltaTime;
            float angleX;

            angleX = Mathf.Atan2(xInput,zInput) * Mathf.Rad2Deg;
            //Debug.Log(deltaPos);
            Quaternion quaternion = Quaternion.Euler(sm.transform.rotation.x, angleX, sm.transform.rotation.z);
            if (Vector3.Distance(sm.transform.rotation.eulerAngles, quaternion.eulerAngles) > 0.1f)
            {

               
                sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, quaternion, sm.turnSmoothSpeed * Time.deltaTime);
            }
            else
            {
                sm.transform.rotation = quaternion;
            }
           
        }
        




    }


   
}
