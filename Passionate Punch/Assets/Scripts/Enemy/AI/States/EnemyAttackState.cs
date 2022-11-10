using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private EnemyMovementSM enemyMovementSM;
    float distance;
    GameObject player, enemy;
    Vector3 enemyPos, playerPos;
    public EnemyAttackState(EnemyMovementSM enemyStateMachine) : base("Attack", enemyStateMachine)
    {
        enemyMovementSM = enemyStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Attack State");
        enemyMovementSM.enemyAnimator.SetTrigger("Attack");
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        enemy = enemyMovementSM.enemy.gameObject;
        enemyPos = enemy.transform.position;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemyMovementSM.enemyAnimator.SetTrigger("Attack");
        // Enemy's warning UI must always looks at to the camera
        enemyMovementSM.warnEnemy.transform.LookAt(Camera.main.transform);
        // Update player's and enemy's positions in every frame
        playerPos = player.transform.position;
        enemyPos = enemy.transform.position;
        // Enemy attacks to the player with respect to distance.
        CalculateDistanceAndAttack();
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Attack State");
    }
    void CalculateDistanceAndAttack()
    {
        distance = Vector3.Distance(enemyPos, playerPos);
        if (distance > enemyMovementSM.enemyAttackDistance.value)
        {
            enemyStateMachine.ChangeState(enemyMovementSM.enemyChasingState);
        }
        else if (distance <= enemyMovementSM.enemyAttackDistance.value)
        {
            enemyMovementSM.enemy.transform.LookAt(player.transform);
            // Attack to the player
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Enemy Stunned");
                enemyStateMachine.ChangeState(enemyMovementSM.enemyStunState);
            }
            // Dying state. IF enemy dies while its attacking. Enter the dying state here
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Enemy Killed");
                enemyMovementSM.enemyAnimator.SetTrigger("Die");
            }
        }
    }
}
