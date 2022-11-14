using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public CharacterBaseStateMachine character;

    //player referans eventle aktar.
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;   
    }
    private void OnDisable()
    {
        ResetSkills();
    }

    public static event Action<CharacterBaseStateMachine> OnSendCharacter;
    public static event Action OnResetSkills;

    public void ResetSkills()
    {
        if (OnResetSkills!=null)
        {
            OnResetSkills();
        }
    }


    public void SendCharacter(CharacterBaseStateMachine character)
    {
        if (OnSendCharacter!=null)
        {
            OnSendCharacter(character);
            this.character = character;
        }
    }

    void Start()
    {
        
    }

    

  
}
