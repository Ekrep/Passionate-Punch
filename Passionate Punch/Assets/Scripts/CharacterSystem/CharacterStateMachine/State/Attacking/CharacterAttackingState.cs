using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Interfaces;

public class CharacterAttackingState : CharacterCanSkillCastableState
{

    private bool _stuckItHere;

    private float _stuckTime;

    private float _pressTime;

    private float _pressCount;

    private bool _isPress;

    //private Transform _target;

    public CharacterAttackingState(CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        SetRotationWhileAttacking();
        sm.anim.SetBool("Attack", true);
        _isPress = false;





    }
    public override void Update()
    {
        base.Update();
        //Debug.Log(_pressCount);
        //ControlPressCount();
       
        ControlStuck();



    }

    public void ChangeAttackState()
    {

        if (!UIManager.Instance.isAttackPress && !_stuckItHere)
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
       // Debug.Log(_stuckTime);
        if (UIManager.Instance.isAttackPress)
        {
            _stuckItHere = true;
            _stuckTime = 0.7f;
            _pressTime += Time.deltaTime;
        }
        else
        {
         
            if (_stuckTime <= 0 || _pressTime > 0.5f)
            {
                _stuckItHere = false;
            }
            else
            {
                _stuckTime -= Time.deltaTime * sm.anim.GetFloat("AttackSpeed"); ;
            }
            _pressTime = 0;

        }
    }
  
    public void ControlPressCount()
    {
        if (UIManager.Instance.isAttackPress&&!_isPress)
        {
            _isPress = true;
            _pressCount++;
            
        }
        if (!UIManager.Instance.isAttackPress)
        {
            _isPress = false;
        }

    }
    public void Attack()
    {
        SetRotationWhileAttacking();
        RaycastHit raycastHit;
        //Physics.CapsuleCastNonAlloc(new Vector3(sm.transform.position.x, sm.transform.position.y + 0.5f, sm.transform.position.z),sm.transform.forward, sm.characterStats.attackRayThickness,sm.transform.forward, raycastHits, sm.characterStats.range);
        //Physics.RaycastNonAlloc(new Vector3(sm.transform.position.x, sm.transform.position.y+0.5f, sm.transform.position.z), sm.transform.forward, raycastHits, sm.characterStats.range);
        Physics.Raycast(new Vector3(sm.transform.position.x, sm.transform.position.y + 0.5f, sm.transform.position.z), sm.transform.forward,out raycastHit, sm.characterStats.range);
        Debug.DrawRay(new Vector3(sm.transform.position.x, sm.transform.position.y + 0.5f, sm.transform.position.z), sm.transform.forward, Color.red, 20);      
        if (raycastHit.collider != null&& raycastHit.collider.GetComponent<IHealth>()!=null)
        {

            Collider[] colliders = new Collider[50];
            int count = 0;

            Debug.Log(raycastHit.collider.gameObject.name);


            count = Physics.OverlapSphereNonAlloc(raycastHit.point, sm.characterStats.AEORange, colliders);
            for (int i = 0; i < count; i++)
            {
                if (colliders[i].gameObject.TryGetComponent<Chest>(out Chest chest))
                {
                    Debug.Log("collision");
                }
                if (colliders[i].gameObject.TryGetComponent<IHealth>(out IHealth hitableObject))
                {
                    hitableObject.Hit(SkillSystem.SkillSettings.HitType.Low,sm.characterStats.attackDamage,Vector3.zero,0);
                    Debug.Log(colliders[i].gameObject.name);
                }

            }


        }

    }

    public override void Exit()
    {

        base.Exit();


        sm.anim.SetBool("Attack", false);


    }



    public void SetRotationWhileAttacking()
    {
        if (sm.autoAim.targetEnemy!=null)
        {
            Vector3 deltaPos = Vector3.zero;
            deltaPos = sm.gameObject.transform.position - sm.autoAim.targetEnemy.position;
            float target = Mathf.Atan2(-deltaPos.x, -deltaPos.z) * Mathf.Rad2Deg;
            //sm.gameObject.transform.rotation.SetLookRotation(sm.autoAim.targetEnemy.position);
            
            sm.gameObject.transform.rotation = Quaternion.Euler(sm.gameObject.transform.rotation.x, target, sm.gameObject.transform.rotation.z);
           
        }



    }



    public bool CheckMovementInput()
    {
        float xInput = 0;
        float zInput = 0;
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
