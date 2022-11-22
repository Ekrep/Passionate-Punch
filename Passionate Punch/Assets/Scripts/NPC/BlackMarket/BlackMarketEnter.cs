using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketEnter : MonoBehaviour
{
    [Header("Ink JSON")]
    public TextAsset inkJSON;

    public static Action TriggerBMDialogue;
    public ScriptableBool isInDialogue, blackMarket;
    private void OnEnable()
    {
        // Initialize the blackmarket scriptable value with false
        blackMarket.value = false;
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
        // SFX Manager
        SFXManager.instance.audioSource.clip = SFXManager.instance.BlackMarketSFX[0];
        SFXManager.instance.audioSource.Play();
        /////////////////////////////////////
        blackMarket.value = true;
    }
    public void OpenBlackMarket()
    {
        if (!DialogueManager.GetInstance().isDialoguePlaying)
        {
            TriggerBMDialogue?.Invoke();
            isInDialogue.value = true;
            // Disable the interact button when player enters the conversation
            UIManager.Instance.interactionButton.interactable = false;
            // Test if the ink file gets or not
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }        
    }
}
