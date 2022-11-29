using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ScriptableBool isInDialogue;

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

    private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        this.character = obj;
    }

    private void OnDisable()
    {
        OnSendCharacter -= GameManager_OnSendCharacter;
    }

    public static event Action<CharacterBaseStateMachine> OnSendCharacter;

    public static event Action<float> OnShakeCam;

    public static event Action OnStopShakeCam;

    public static event Action<float> OnSkillCasted;

    public void SendCharacter(CharacterBaseStateMachine character)
    {
        if (OnSendCharacter != null)
        {
            OnSendCharacter(character);

        }
    }

    public void ShakeCam(float shakeRange)
    {
        if (OnShakeCam != null)
        {
            OnShakeCam(shakeRange);
        }
    }

    public void StopShakeCam()
    {
        if (OnStopShakeCam!=null)
        {
            OnStopShakeCam();
        }
    }

    public void SkillCasted(float skillManaCost)
    {
        if (OnSkillCasted!=null)
        {
            OnSkillCasted(skillManaCost);
        }
    }

    void Start()
    {
       // isInDialogue.value = false;
    }




}
