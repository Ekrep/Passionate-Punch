using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketExit : MonoBehaviour
{
    public ScriptableBool isInDialogue, blackMarket;
    public ScriptableString npcName;
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
        npcName.value = "";
        if (UIManager.Instance.interactionButton.interactable)
        {
            UIManager.Instance.interactionButton.interactable = false;
        }
        UIManager.Instance.interactionButton.onClick.RemoveAllListeners();
        isInDialogue.value = false;
        blackMarket.value = false;
    }
}
