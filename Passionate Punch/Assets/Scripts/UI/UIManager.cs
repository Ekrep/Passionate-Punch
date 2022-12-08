using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using CharacterUtilities;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public MiniMap miniMap;
    public Button interactionButton;

    public TextMeshProUGUI levelUpText;

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


    [HideInInspector]
    public bool isPickUpButtonPressed;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        interactionButton.gameObject.SetActive(false);
        levelUpText.gameObject.SetActive(false);
    }

    public static event Action<MiniMapIcon> OnRefreshMiniMap;

    public static event Action<MiniMapIcon> OnCreateIcons;

    public static event Action OnTriggeredWithItem;
    public static event Action OnTriggerExitWithItem;

    public void CreateIcons(MiniMapIcon miniMapIcon)
    {
        if (OnCreateIcons!=null)
        {
            OnCreateIcons(miniMapIcon);
        }
    }

    public void TriggeredWithItem()
    {
        if (OnTriggeredWithItem!=null)
        {
            OnTriggeredWithItem();
        }
    }

    public void TriggerExitWithItem()
    {
        if (OnTriggerExitWithItem!=null)
        {
            OnTriggerExitWithItem();
        }
    }


    public void RefreshMiniMap(MiniMapIcon miniMapIcon)
    {
        if (OnRefreshMiniMap!=null)
        {
            OnRefreshMiniMap(miniMapIcon);
        }
    }
    
}
