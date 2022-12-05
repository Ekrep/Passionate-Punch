using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    float chaseSpeed, awakeTime, stopDistance;
    public float distanceToPlayer;
    public EnemyChasingState(EnemyMovementSM enemyStateMachine) : base("Chasing", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        // Stop distance rearrange when player chasing by an enemy
        stopDistance = 2f;
        enemyMovementSM.enemyNavMesh.stoppingDistance = stopDistance;
        /////////////////////////////////////////////////////////
        awakeTime = .75f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.warnEnemy.transform.LookAt(Camera.main.transform);
        awakeTime -= Time.deltaTime;        
        if (awakeTime <= 0)
        {
            enemyMovementSM.enemyAnimator.ResetTrigger("Idle");
            ChasePlayer();
            CalculateDistanceToPlayer();
            enemyMovementSM.enemyAnimator.SetTrigger("Run");
        }
        else
        {
            // The time before enemy wake up
            // This Idle will be changed with "warned" animation
            enemyMovementSM.enemyAnimator.SetTrigger("Idle");
        }
    }
    // IF player exits from the enemy's trigger, enemy returns to its position (Return State)
    public override void EnemyTriggerExit(Collider other)
    {
        base.EnemyTriggerExit(other);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyReturnState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemyMovementSM.enemyAnimator.ResetTrigger("Run");
    }
    void ChasePlayer()
    {
        chaseSpeed = enemyMovementSM.enemyMovementSpeed.value;
        enemyMovementSM.enemyNavMesh.speed = chaseSpeed;
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        enemyMovementSM.enemyNavMesh.SetDestination(playerPos);
    }
    void CalculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(enemyPos, playerPos);
        if (distanceToPlayer <= enemyMovementSM.enemyAttackDistance.value)
        {
            // Change state to attack
            enemyStateMachine.ChangeState(enemyMovementSM.enemyAttackState);
        }
    }
}
