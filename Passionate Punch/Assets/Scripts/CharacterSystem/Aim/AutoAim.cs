using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
   

    [HideInInspector]
    public Transform targetEnemy;

    [SerializeField]
    private LayerMask _layer;
   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Aiming();
        }
        
    }
    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,_layer)&&hit.collider.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine enemy))
        {
           
            targetEnemy = enemy.transform;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            enemy.FocusEnemy();
            Debug.Log(enemy);
            Debug.Log("girdimfocus");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            enemy.NotFocusEnemy();
        }
    }
}
