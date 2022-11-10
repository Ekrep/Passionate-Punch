using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingState : CharacterCanAttackableState
{

    Vector3 currentPos;
    Vector3 firstPos;
    Vector3 deltaPos;
    public CharacterMovingState( CharacterBaseStateMachine stateMachine) : base("Moving", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        firstPos = Vector3.zero;
        sm.anim.SetBool("Moving", true);
    }

   
    public override void Update()
    {
        base.Update();
        currentPos = sm.gameObject.transform.position;
       




    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        deltaPos = currentPos - firstPos;
        firstPos = sm.gameObject.transform.position;
        Move();
       



    }
    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("Moving", false);

    }


    private void Move()
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
                xInput = Input.GetAxisRaw("Horizontal");
                zInput = Input.GetAxisRaw("Vertical");
                break;
        }
       
        //UI Input geldiginde degisecek
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {

            sm.transform.position = new Vector3(sm.transform.position.x + xInput * sm.characterMovementSpeed * Time.deltaTime, sm.transform.position.y, sm.transform.position.z + zInput * sm.characterMovementSpeed * Time.deltaTime);
            float angleX;

            angleX = Mathf.Atan2(deltaPos.x, deltaPos.z) * Mathf.Rad2Deg;
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
        if (xInput==0&&zInput==0)
        {
            sm.ChangeState(sm.characterIdleState);
        }




    }


   
}
