using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private void OnEnable()
    {
        BlackMarketEnter.TriggerBMDialogue += TriggerDialogue;
    }
    private void OnDisable()
    {
        BlackMarketEnter.TriggerBMDialogue -= TriggerDialogue;
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
