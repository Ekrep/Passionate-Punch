using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingState : CharacterAliveState
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
        CheckMovement();
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
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        //UI Input geldiginde degisecek
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {

            sm.transform.position = new Vector3(sm.transform.position.x + xInput * sm.characterMovementSpeed * Time.fixedDeltaTime, sm.transform.position.y, sm.transform.position.z + zInput * sm.characterMovementSpeed * Time.fixedDeltaTime);
            float angleX;

            angleX = Mathf.Atan2(deltaPos.x, deltaPos.z) * Mathf.Rad2Deg;
            Quaternion quaternion = Quaternion.Euler(sm.transform.rotation.x, angleX, sm.transform.rotation.z);
            if (Vector3.Distance(sm.transform.rotation.eulerAngles, quaternion.eulerAngles) > 0.1f)
            {

                Debug.Log("lerping");
                sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, quaternion, sm.turnSmoothSpeed * Time.fixedDeltaTime);
            }
            else
            {
                sm.transform.rotation = quaternion;
            }
        }




    }


    private void CheckMovement()
    {
        float xInput = 0;
        float zInput = 0;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(xInput) == 0 && Mathf.Abs(zInput) == 0)
        {
            sm.ChangeState(sm.characterIdleState);
        }
    }
}
