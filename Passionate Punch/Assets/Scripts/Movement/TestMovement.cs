using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TestMovement : MonoBehaviour
{
    public float characterMovementSpeed;
   [SerializeField]
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Move();  
    }


   private void Move()
    {
        float xInput=0;
        float zInput=0;
        xInput=Input.GetAxis("Horizontal");
        zInput=Input.GetAxis("Vertical");

        _rb.MovePosition(new Vector3(gameObject.transform.position.x+xInput*characterMovementSpeed*Time.fixedDeltaTime, gameObject.transform.position.y, gameObject.transform.position.z +zInput *characterMovementSpeed * Time.fixedDeltaTime));

        
    }

}
