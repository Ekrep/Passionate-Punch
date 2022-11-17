using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketExit : MonoBehaviour
{
    public ScriptableBool isInDialogue;
    private void OnEnable()
    {
        BlackMarket.OnBlackMarketExit += OnMarketExit;
    }
    private void OnDisable()
    {
        BlackMarket.OnBlackMarketExit -= OnMarketExit;
    }
    public void OnMarketExit()
    {
        UIManager.Instance.interactionButton.interactable = false;
        UIManager.Instance.interactionButton.onClick.RemoveAllListeners();
        isInDialogue.value = false;
    }
}
