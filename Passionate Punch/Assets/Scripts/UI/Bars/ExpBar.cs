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
    float levelUpWaitingTime;
    private void OnEnable()
    {
        CharacterExperience.OnGainExperience += GainExperience;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
        CharacterExperience.OnGainExperience -= GainExperience;
        GameManager.OnSendCharacter -= PullChar;
    }
    private void Start()
    {
        levelUpWaitingTime = 2f;
        expBar = GetComponent<Slider>();
        // Set the max experience point to reach next level.
        expBar.maxValue = charSettings.experienceThreshold;
        expBar.value = charSettings.experience; 
    }
    void GainExperience(float expAmount)
    {
        Debug.Log("Experience gained: " + expAmount);
        expBar.value += expAmount;
        // Level Up
        if (expAmount == 0) // If the action sends the value "0", it means that player levels up
        {
            StartCoroutine(LevelUp());
        }
    }
    // Experience bar is waiting for some time before levels up and update itself.
    IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(levelUpWaitingTime);
        expBar.maxValue = charSettings.experienceThreshold;
        expBar.value = charSettings.experience;
    }
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
    }
}
