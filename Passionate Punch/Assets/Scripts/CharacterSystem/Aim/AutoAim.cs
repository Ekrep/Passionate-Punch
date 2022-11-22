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
                _focusedEnemy.NotFocusEnemy();
                _focusedEnemy = null;
                targetEnemy = null;
            }
        }

    }
    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layer))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && _focusedEnemy == null)
            {
                targetEnemy = enemy.transform;
                _focusedEnemy = enemy;
                enemy.FocusEnemy();
            }
             if (enemy && _focusedEnemy != null)
            {
                _focusedEnemy.NotFocusEnemy();
                _focusedEnemy = enemy;
                targetEnemy = _focusedEnemy.transform;
                _focusedEnemy.FocusEnemy();
            }

        }






    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && targetEnemy == null && enemy.currentEnemyState != enemy.enemyDieState)
        {
            Debug.Log("enter Auto");
            targetEnemy = enemy.transform;
            _focusedEnemy = enemy;
            _focusedEnemy.FocusEnemy();


        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.transform.parent != null && _focusedEnemy == other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && enemy != null)
        {
            Debug.Log("exit Auto");
            _focusedEnemy.NotFocusEnemy();
            targetEnemy = null;
            _focusedEnemy = null;

        }
    }


}
