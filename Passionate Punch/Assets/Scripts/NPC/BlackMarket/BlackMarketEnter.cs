using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketEnter : MonoBehaviour
{
    public static Action TriggerBMDialogue;
    public ScriptableBool isInDialogue;
    private void OnEnable()
    {
        BlackMarket.OnBlackMarketEnter += OnMarketEnter;
    }
    private void OnDisable()
    {
        BlackMarket.OnBlackMarketEnter -= OnMarketEnter;
    }
    public void OnMarketEnter()
    {
        if (!UIManager.Instance.interactionButton.gameObject.activeInHierarchy)
        {
            UIManager.Instance.interactionButton.gameObject.SetActive(true);
        }
        UIManager.Instance.interactionButton.interactable = true;
        UIManager.Instance.interactionButton.onClick.AddListener(OpenBlackMarket);
    }
    public void OpenBlackMarket()
    {
        TriggerBMDialogue?.Invoke();
        isInDialogue.value = true;
        // Disable the interact button when player enters the conversation
        UIManager.Instance.interactionButton.interactable = false;
    }
}
