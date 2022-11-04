using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    [HideInInspector]
    public float joystickHorizontalInput;
    [HideInInspector]
    public float joystickVerticalInput;

    [HideInInspector]
    public bool isAttackPress;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Debug.Log(isAttackPress);
    }
}
