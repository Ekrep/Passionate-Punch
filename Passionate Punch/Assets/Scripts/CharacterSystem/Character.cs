using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class Character : MonoBehaviour
{
    public CharacterBaseStateMachine characterBaseStateMachine;

    private void OnEnable()
    {
        //DataManager.Instance.SendDataPullRequest(characterBaseStateMachine);
    }

    void Start()
    {
          
        GameManager.Instance.SendCharacter(characterBaseStateMachine);
    }

    

}
