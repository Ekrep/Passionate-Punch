using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    float stunTime;
    float distance;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    public EnemyStunState(EnemyMovementSM enemyStateMachine) : base("Stun", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Stun State");
        stunTime = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        stunTime -= Time.deltaTime;
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        // When enemy exits from stun, it will attack or chase the player with respect to its distance to player
        if (stunTime <= 0)
        {
            Debug.Log("Exits Stun");
            AttackOrChase();
        }
    }
    // IF player exits the trigger while enemy is in the stun, Enemy will return to its returning state. 
    public override void EnemyTriggerExit(Collider other)
    {
        base.EnemyTriggerExit(other);
        Debug.Log("Target lost");
        enemyStateMachine.ChangeState(enemyMovementSM.enemyReturnState);
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Stun State");
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
            enemyStateMachine.ChangeState(enemyMovementSM.enemyAttackState);
        }
    }
}
