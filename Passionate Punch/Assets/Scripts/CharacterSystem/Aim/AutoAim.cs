using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoAim : MonoBehaviour
{


    //[HideInInspector]
    public Transform targetEnemy;
    [SerializeField]
    private EnemyMovementSM _focusedEnemy;
    [SerializeField]
    private LayerMask _layer;
   

    private void Update()
    {
      
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Aiming();
            }
            if (_focusedEnemy != null && _focusedEnemy.currentEnemyState == _focusedEnemy.enemyDieState)
            {
                _focusedEnemy = null;
                targetEnemy = null;
            }
        }
        
    }
    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,_layer))
        {
            if (hit.collider.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy)&&_focusedEnemy==null)
            {
                targetEnemy = enemy.transform;
                _focusedEnemy = enemy;
                enemy.FocusEnemy();
            }
            else if (_focusedEnemy!=null)
            {
                _focusedEnemy.NotFocusEnemy();
                _focusedEnemy = enemy;
                targetEnemy = _focusedEnemy.transform;
                _focusedEnemy.FocusEnemy();
            }

        }
        else
        {
            if (_focusedEnemy!=null)
            {
                _focusedEnemy.NotFocusEnemy();
            }
               
            targetEnemy = null;
            _focusedEnemy = null;

            
        }
       
       
       
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy)&&targetEnemy==null&&enemy.currentEnemyState!=enemy.enemyDieState)
        {
            targetEnemy = enemy.transform;
            _focusedEnemy = enemy;
            enemy.FocusEnemy();
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            enemy.NotFocusEnemy();
            targetEnemy = null;
            _focusedEnemy = null;

        }
    }

    
}
