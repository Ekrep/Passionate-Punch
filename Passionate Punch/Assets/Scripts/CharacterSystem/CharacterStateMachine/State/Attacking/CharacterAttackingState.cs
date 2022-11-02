using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackingState : CharacterCanAttackableState
{
    private float _attackCount;
    public CharacterAttackingState( CharacterBaseStateMachine stateMachine) : base("Attacking", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        sm.anim.SetBool("Attack", true);
        sm.StartCoroutine(AttackCount());

    }
    public override void Update()
    {
        base.Update();
        Debug.Log(_attackCount);
        
    }


    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("Attack", false);
        sm.StopCoroutine(AttackCount());
        
    }



   IEnumerator AttackCount()
    {
        
        if (_attackCount>1)
        {
            _attackCount = 0;
        }
        sm.anim.SetFloat("AttackCount", _attackCount);
        yield return new WaitForSecondsRealtime(sm.characterStats.attackSpeed);
        _attackCount += 0.5f;
        
        sm.StartCoroutine(AttackCount());

    }




}
