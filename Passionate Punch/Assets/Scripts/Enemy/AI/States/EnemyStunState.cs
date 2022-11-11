using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    bool isOut;
    float stunTime;
    float distance;
    public EnemyStunState(EnemyMovementSM enemyStateMachine) : base("Stun", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        //
        enemyMovementSM.stunButton.onClick.RemoveAllListeners();
        enemyMovementSM.killButton.onClick.RemoveAllListeners();
        //
        enemyMovementSM.enemyAnimator.ResetTrigger("Run");
        enemyMovementSM.enemyAnimator.SetTrigger("Stun");
        // Stun particles activation
        enemyMovementSM.stunParticles.gameObject.SetActive(true);
        stunTime = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
        isOut = false;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.warnEnemy.transform.LookAt(Camera.main.transform);
        stunTime -= Time.deltaTime;
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        // When enemy exits from stun, it will attack or chase the player with respect to its distance to player
        if (stunTime <= 0 && !isOut)
        {
            AttackOrChase();
        }
        else if (isOut && stunTime <= 0)
        {
            enemyMovementSM.enemyAnimator.SetTrigger("Idle");
            enemyStateMachine.ChangeState(enemyMovementSM.enemyReturnState);
        }
        else
        {
            enemyMovementSM.enemyAnimator.SetTrigger("Stun");
        }
    }
    // IF player exits the trigger while enemy is in the stun, Enemy will return to its returning state. 
    public override void EnemyTriggerExit(Collider other)
    {
        base.EnemyTriggerExit(other);
        if (other.gameObject.CompareTag("Player"))
        {
            isOut = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemyMovementSM.enemyAnimator.ResetTrigger("Stun");
        isOut = false;
        // Stun particles object passive
        enemyMovementSM.stunParticles.gameObject.SetActive(false);
    }
    void AttackOrChase()
    {
        distance = Vector3.Distance(enemyPos, playerPos);
        // If player too far from enemy, then enemy will chase him.
        if (distance > enemyMovementSM.enemyAttackDistance.value)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
        // If player is close to the enemy, enemy will return to his attack state
        else if (distance <= enemyMovementSM.enemyAttackDistance.value)
        {
            enemyMovementSM.enemyAnimator.SetTrigger("Attack");
            enemyStateMachine.ChangeState(enemyMovementSM.enemyAttackState);
        }
    }
}
