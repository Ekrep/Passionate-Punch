using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // This script is just for remember how to attach the JSON Script to the NPC that has been choosed.
    // When new NPC and its dialogue created, the integration process is like this. 
    // Rewrite the following lines into the dialogue trigger part of that NPC.
    // You can look at BlackMarketEnter script to have idea of the process.
    [Header("Ink JSON")]
    public TextAsset inkJSON;
    public ScriptableBool isInDialogue;
    public void OpenBlackMarket()
    {
        if (!DialogueManager.GetInstance().isDialoguePlaying)
        {
            isInDialogue.value = true;
            // Disable the interact button when player enters the conversation
            UIManager.Instance.interactionButton.interactable = false;
            // Test if the ink file gets or not
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
}
