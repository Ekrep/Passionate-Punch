using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public MiniMap miniMap;
    public Button interactionButton;

    [HideInInspector]
    public float joystickHorizontalInput;
    [HideInInspector]
    public float joystickVerticalInput;

    [HideInInspector]
    public bool isAttackPress;

    [HideInInspector]
    public bool isPressedSkillOne;

    [HideInInspector]
    public bool isPressedSkillTwo;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

     //   interactionButton.gameObject.SetActive(false);

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
