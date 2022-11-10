using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Character : MonoBehaviour
{
    public CharacterBaseStateMachine characterBaseStateMachine;
    void Start()
    {
        GameManager.Instance.SendCharacter(characterBaseStateMachine);
    }

    
}
