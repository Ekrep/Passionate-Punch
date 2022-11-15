using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum CamCurrentType
    {
        Static,
        Dynamic
    }

    [SerializeField]
    private CamCurrentType _currentType;
    private Vector3 _distance;
    [SerializeField] private GameObject _followedbyCamObject;



    void Start()
    {
        _distance = _followedbyCamObject.transform.position - gameObject.transform.position;
    }

    private void LateUpdate()
    {
        
        gameObject.transform.position = _followedbyCamObject.transform.position - _distance;
    }
}
