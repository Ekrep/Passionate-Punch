using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterCanAttackableState
{
    private float _attackCount;

    private bool _isAttacking;
    public CharacterAttackingState(CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();




    }
    public override void Update()
    {
        base.Update();

        Debug.Log(sm.anim.GetCurrentAnimatorStateInfo(0).length);
        if (!_isAttacking)
        {
            Debug.Log(_attackCount);
            sm.StartCoroutine(AttackCount());

            _isAttacking = true;
        }


    }


    public override void Exit()
    {

        base.Exit();
        _attackCount = 0;
        sm.anim.SetFloat("AttackCount", _attackCount);
        Debug.Log("exx");
        sm.anim.SetBool("Attack", false);
        sm.StopCoroutine(AttackCount());


    }



    IEnumerator AttackCount()
    {
        Debug.Log("corot");
        sm.anim.SetBool("Attack", true);

        sm.anim.SetFloat("AttackCount", _attackCount);
        
        yield return new WaitForSeconds(sm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        _attackCount += 0.5f;

        if (!stillPressingAttack)
        {
            Debug.Log("girdimkeyup");
            sm.ChangeState(sm.characterIdleState);
        }

        if (_attackCount > 2f)
        {
            _attackCount = 0;
        }
       



        /* else
         {
             sm.StartCoroutine(AttackCount());
         }*/
        _isAttacking = false;









    }




}
