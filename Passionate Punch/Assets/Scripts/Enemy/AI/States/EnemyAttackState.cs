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
        // TEMPORARY BUTTON FUNCTIONS
        enemyMovementSM.stunButton.onClick.AddListener(StunEnemy);
        enemyMovementSM.killButton.onClick.AddListener(KillEnemy);
        //////////////////////////////
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
        enemyMovementSM.enemyAnimator.ResetTrigger("Attack");
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
            // Attack to the player
            enemyMovementSM.enemy.transform.LookAt(player.transform);
        }
    }
    void StunEnemy()
    {
        enemyStateMachine.ChangeState(enemyMovementSM.enemyStunState);
    }
    void KillEnemy()
    {
        enemyMovementSM.enemyAnimator.ResetTrigger("Attack");
        enemyMovementSM.ChangeState(enemyMovementSM.enemyDieState);
    }
}
