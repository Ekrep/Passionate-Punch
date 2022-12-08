using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Interfaces;
using SkillSystem;

public class EnemyMovementSM : EnemyStateMachine,IHealth
{
    ///////////////////////////////
    float currentHealth, maxHealth;
    public Slider enemyHealthBar;
    Animator enemyHealthBarAnimator;
    ////////////////////////////////
    public Collider enemySphereCollider;
    public ScriptableFloat enemyMovementSpeed, enemyReturningSpeed, enemyPatrollingSpeed, enemyAttackDistance;
    public Transform enemyCampPos;
    public List<Transform> patrolPositions;
    public GameObject stunParticles, warnEnemy, focusCanvas;
    public Animator enemyCanvasAnimator, enemyAnimator;
    int empty = 0;
    [SerializeField]
    private ParticleSystem _hitPs;
    [HideInInspector]
    public bool isPatrollingEnemy;
    [HideInInspector]
    public NavMeshAgent enemyNavMesh;
    // Our enemy game object
    [HideInInspector]
    public GameObject enemy;
    ////////////////////////
    [HideInInspector]
    public EnemyIdleState enemyIdleState;
    [HideInInspector]
    public EnemyChasingState enemyChasingState;
    [HideInInspector]
    public EnemyReturnState enemyReturnState;
    [HideInInspector]
    public EnemyAttackState enemyAttackState;
    [HideInInspector]
    public EnemyStunState enemyStunState;
    [HideInInspector]
    public EnemyPatrollingState enemyPatrollingState;
    [HideInInspector]
    public EnemyDieState enemyDieState;

    public float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        // Health of an enemy
        maxHealth = 500f;
        currentHealth = maxHealth;
        enemyHealthBarAnimator = enemyHealthBar.GetComponent<Animator>();
        //enemyHealthBar.gameObject.SetActive(false);
        // Set pasive the stun particles & Warn enemy canvas
        stunParticles.gameObject.SetActive(false);
        warnEnemy.gameObject.SetActive(false);
        ////////////////////////////////////////////////////
        enemy = this.gameObject;
        enemyIdleState = new EnemyIdleState(this);
        enemyChasingState = new EnemyChasingState(this);
        enemyReturnState = new EnemyReturnState(this);
        enemyAttackState = new EnemyAttackState(this);
        enemyStunState = new EnemyStunState(this);
        enemyPatrollingState = new EnemyPatrollingState(this);
        enemyDieState = new EnemyDieState(this);
    }
    protected override EnemyBaseState GetInitialState()
    {
        if (patrolPositions.Count > empty)
        {
            isPatrollingEnemy = true;
            return enemyPatrollingState;
        }
        else
        {
            isPatrollingEnemy = false;
            return enemyIdleState;
        }
    }
    private void OnEnable()
    {
        HealthBar.OnPlayerDead += PlayerKilled;
        Respawn.OnRespawn += PlayerReborn;
    }
    private void OnDisable()
    {
        HealthBar.OnPlayerDead -= PlayerKilled;
        Respawn.OnRespawn -= PlayerReborn;
    }
    IEnumerator PlayerRebornNumerator()
    {
        yield return new WaitForSeconds(2f);
        enemySphereCollider.enabled = true;
    }
    void PlayerReborn()
    {
        StartCoroutine(PlayerRebornNumerator());
    }
    void PlayerKilled()
    {
        enemySphereCollider.enabled = false;
        enemyAnimator.ResetTrigger("Idle");
        enemyAnimator.ResetTrigger("Run");
        enemyAnimator.ResetTrigger("Walk");
        enemyAnimator.ResetTrigger("Attack");
        enemyAnimator.ResetTrigger("Die");
        enemyAnimator.ResetTrigger("Stun");
        ChangeState(enemyReturnState);
    }
    void UpdateHealthBar()
    {
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
    }
    public void FocusEnemy()
    {
        focusCanvas.gameObject.SetActive(true);
        enemyHealthBarAnimator.SetTrigger("Activate");
        UpdateHealthBar();
    }
    public void NotFocusEnemy()
    {
        focusCanvas.gameObject.SetActive(false);
        enemyHealthBarAnimator.SetBool("Animate", false);
        enemyHealthBarAnimator.ResetTrigger("Activate");
        enemyHealthBarAnimator.SetTrigger("Deactivate");
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            KillSelf();
            NotFocusEnemy();
        }
    }

    public void KillSelf()
    {
        ChangeState(enemyDieState);
        enemyHealthBarAnimator.SetBool("Animate", false);
    }

    public void Hit(SkillSettings.HitType hitType, float damage, Vector3 hitPos, float pushAmount)
    {
        switch (hitType)
        {
            case SkillSettings.HitType.Low:
                _hitPs.Play();
                DecreaseHealth(damage);
                break;
            case SkillSettings.HitType.Medium:
                _hitPs.Play();
                DecreaseHealth(damage);
                gameObject.GetComponent<Rigidbody>().AddForce(hitPos * pushAmount*Time.fixedDeltaTime);
                
                
                break;
            case SkillSettings.HitType.Hard:
                _hitPs.Play();
                DecreaseHealth(damage);
                ChangeState(enemyStunState);
                break;
            default:
                break;
        }
    }
}
