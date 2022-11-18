using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
   

    [HideInInspector]
    public Transform targetEnemy;

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
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)&&hit.collider.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine enemy))
        {
            //_hitPos = hit.point;
            targetEnemy = enemy.transform;
        }

        /*Vector3 directon = _hitPos - transform.position;
        directon.y = 0;

        transform.forward = directon;*/
    }
}
