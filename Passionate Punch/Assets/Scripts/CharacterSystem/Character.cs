using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class Character : MonoBehaviour
{
    public CharacterBaseStateMachine characterBaseStateMachine;

    private void OnEnable()
    {

    }

    void Start()
    {
        DataManager.Instance.SendDataPullRequest(characterBaseStateMachine);
        GameManager.Instance.SendCharacter(characterBaseStateMachine);
    }



}
