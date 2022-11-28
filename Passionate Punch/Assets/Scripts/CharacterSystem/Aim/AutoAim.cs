using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoAim : MonoBehaviour
{


    [HideInInspector]
    public Transform targetEnemy;
    [HideInInspector]
    public EnemyMovementSM focusedEnemy;
    [SerializeField]
    private LayerMask _layer;

    //needs adjustment
    private void Update()
    {
       

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Aiming();
            }
            if (focusedEnemy != null && focusedEnemy.currentEnemyState == focusedEnemy.enemyDieState)
            {
                focusedEnemy.NotFocusEnemy();
                focusedEnemy = null;
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
            if (hit.collider.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && focusedEnemy == null)
            {
                targetEnemy = enemy.transform;
                focusedEnemy = enemy;
                enemy.FocusEnemy();
            }
             if (enemy && focusedEnemy != null)
            {
                focusedEnemy.NotFocusEnemy();
                focusedEnemy = enemy;
                targetEnemy = focusedEnemy.transform;
                focusedEnemy.FocusEnemy();
            }

        }






    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && targetEnemy == null && enemy.currentEnemyState != enemy.enemyDieState)
        {
            targetEnemy = enemy.transform;
            focusedEnemy = enemy;
            focusedEnemy.FocusEnemy();


        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.parent != null && focusedEnemy==null&& other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            targetEnemy = enemy.transform;
            focusedEnemy = enemy;
            focusedEnemy.FocusEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.transform.parent != null && focusedEnemy == other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy) && enemy != null)
        {
            Debug.Log("exit Auto");
            focusedEnemy.NotFocusEnemy();
            targetEnemy = null;
            focusedEnemy = null;

        }
    }


}
