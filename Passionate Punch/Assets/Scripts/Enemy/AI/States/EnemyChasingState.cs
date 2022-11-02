using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    float chaseSpeed, awakeTime;
    public EnemyChasingState(EnemyMovementSM enemyStateMachine) : base("Chasing", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered the chasing state");
        awakeTime = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
        chaseSpeed = enemyMovementSM.enemyMovementSpeed.value * Time.deltaTime;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        awakeTime -= Time.deltaTime;
        if (awakeTime <= 0)
        {
            ChasePlayer();
        }     
    }
    // IF player exits from the enemy's trigger, enemy returns to its idle state
    public override void EnemyTriggerExit(Collider other)
    {
        base.EnemyTriggerExit(other);
        if (other.gameObject.CompareTag("Player"))
        {       
            enemyStateMachine.ChangeState(enemyMovementSM.enemyIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Chasing State");
    }
    void ChasePlayer()
    {
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        enemy.transform.position = Vector3.MoveTowards(enemyPos, playerPos, chaseSpeed);
    }
}
