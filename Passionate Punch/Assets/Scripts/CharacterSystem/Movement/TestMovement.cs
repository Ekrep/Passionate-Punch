using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;


public class TestMovement : MonoBehaviour
{


    public CharacterSettings characterStats;
    private float _characterMovementSpeed;


    [SerializeField]
    private float _smoothSpeed;

    Vector3 currentPos;
    Vector3 firstPos;
    Vector3 deltaPos;
    void Start()
    {
        _characterMovementSpeed = characterStats.moveSpeed;
        firstPos = Vector3.zero;
    }
    private void Update()
    {
        currentPos = gameObject.transform.position;
        
    }

    void FixedUpdate()
    {
        
    }
    private void LateUpdate()
    {
        deltaPos = currentPos - firstPos;
        firstPos = gameObject.transform.position;
        Move();

    }

    private void Move()
    {
        //UI Input geldiginde degisecek
        if (Input.anyKey)
        {
            float xInput = 0;
            float zInput = 0;
            xInput = Input.GetAxis("Horizontal");
            zInput = Input.GetAxis("Vertical");

            transform.position = new Vector3(gameObject.transform.position.x + xInput * _characterMovementSpeed * Time.fixedDeltaTime, gameObject.transform.position.y, gameObject.transform.position.z + zInput * _characterMovementSpeed * Time.fixedDeltaTime);
            float angleX;

            angleX = Mathf.Atan2(deltaPos.x, deltaPos.z) * Mathf.Rad2Deg;
            Quaternion quaternion = Quaternion.Euler(gameObject.transform.rotation.x, angleX, gameObject.transform.rotation.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, _smoothSpeed * Time.fixedDeltaTime);
        }
       
       
           
        
        
        
        
       
       


        
    }

}
