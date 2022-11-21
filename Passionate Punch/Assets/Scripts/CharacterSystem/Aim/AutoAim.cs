using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
   

    [HideInInspector]
    public Transform targetEnemy;

    private EnemyMovementSM _focusedEnemy;

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
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,_layer)&&hit.collider.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
           
            targetEnemy = enemy.transform;
            _focusedEnemy = enemy;
            enemy.FocusEnemy();
        }
       
       
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            enemy.FocusEnemy();
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.TryGetComponent<EnemyMovementSM>(out EnemyMovementSM enemy))
        {
            enemy.NotFocusEnemy();
        }
    }
}
