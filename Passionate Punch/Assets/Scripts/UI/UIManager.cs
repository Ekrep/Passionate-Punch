using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public MiniMap miniMap;


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


    public static event Action OnRefreshMiniMap;


    public void RefreshMiniMap()
    {
        if (OnRefreshMiniMap!=null)
        {
            OnRefreshMiniMap();
        }
    }
    
}
