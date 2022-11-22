using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableBool isInRange;
    private void Awake()
    {
        isInRange.value = false;
        Debug.Log("Test Trigger");
    }
}
