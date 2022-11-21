using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector]
   public EnemyBaseState currentEnemyState;
    void Start()
    {
        currentEnemyState = GetInitialState();
        if (currentEnemyState != null)
        {
            currentEnemyState.Enter();
        }
    }
    void Update()
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.UpdateLogic();
        }
    }
    private void LateUpdate()
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.UpdatePhysics();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.EnemyTriggerEnter(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.EnemyTriggerStay(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.EnemyTriggerExit(other);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.EnemyOnCollisionEnter(collision);
        }
    }
    public void ChangeState(EnemyBaseState newState)
    {
        currentEnemyState.Exit();
        currentEnemyState = newState;
        currentEnemyState.Enter();
    }
    protected virtual EnemyBaseState GetInitialState()
    {
        return null;
    }
}
