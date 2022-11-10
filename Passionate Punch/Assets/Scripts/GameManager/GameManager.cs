using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public CharacterBaseStateMachine character;

    //player referans eventle aktar.
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;   
    }
    private void OnEnable()
    {
        OnSendCharacter += GameManager_OnSendCharacter;
    }

    private void GameManager_OnSendCharacter(CharacterBaseStateMachine character)
    {
        this.character = character;
    }

    private void OnDisable()
    {
        OnSendCharacter -= GameManager_OnSendCharacter;
    }

    public static event Action<CharacterBaseStateMachine> OnSendCharacter;


    public void SendCharacter(CharacterBaseStateMachine character)
    {
        if (OnSendCharacter!=null)
        {
            OnSendCharacter(character);
        }
    }

    void Start()
    {
        
    }

    

  
}
