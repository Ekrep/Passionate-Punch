using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAgent : MonoBehaviour
{
    public static Action OnQuestAgentEnter, OnQuestAgentExit;
    // If player get in touch with the quest agent, actions happens
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnQuestAgentEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnQuestAgentExit?.Invoke();
        }
    }
}
