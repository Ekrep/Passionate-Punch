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


    private void Awake()
    {
        Instance = this;
    }
}
