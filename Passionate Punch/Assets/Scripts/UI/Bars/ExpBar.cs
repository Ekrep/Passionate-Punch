using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using CharacterUtilities;
using UnityEngine;
using UnityEngine.UI;   

public class ExpBar : MonoBehaviour
{
    CharacterSettings charSettings;
    GameManager gameManager;
    Slider expBar;
    private void OnEnable()
    {
        CharacterExperience.onGainExperience += GainExperience;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
        CharacterExperience.onGainExperience -= GainExperience;
        GameManager.OnSendCharacter -= PullChar;
    }
    private void Start()
    {
        expBar = GetComponent<Slider>();
        // Set the max experience point to reach next level.
        expBar.maxValue = charSettings.experienceThreshold;
        expBar.value = charSettings.experience; 
    }
    void GainExperience(float expAmount)
    {
        Debug.Log("Experience gained: " + expAmount);
        expBar.value += expAmount;
    }
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
    }
}
