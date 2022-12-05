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
    Image expBar;
    float levelUpWaitingTime;
    private void OnEnable()
    {
        CharacterExperience.OnGainExperience += GainExperience;
        CharacterExperience.OnLevelUp += OnLevelUp;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
        CharacterExperience.OnGainExperience -= GainExperience;
        CharacterExperience.OnLevelUp -= OnLevelUp;
        GameManager.OnSendCharacter -= PullChar;
    }
    private void Start()
    {

    }
    void GainExperience(float expAmount)
    {
        Debug.Log("Experience gained: " + expAmount);
        expBar.fillAmount = CalculateExp(expAmount);
    }
    float CalculateExp(float expAmount)
    {
        expBar.fillAmount = charSettings.experience / charSettings.experienceThreshold;
        expAmount = expBar.fillAmount;
        return expAmount;
    }
    void OnLevelUp()
    {
        StartCoroutine(LevelUp());
    }
    // Experience bar is waiting for some time before levels up and update itself.
    IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(levelUpWaitingTime);
        expBar.fillAmount = charSettings.experienceThreshold;
        expBar.fillAmount = charSettings.experience;
    }
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
        levelUpWaitingTime = 2f;
        expBar = GetComponent<Image>();
        // Set the max experience point to reach next level.
        expBar.fillAmount = charSettings.experience / charSettings.experienceThreshold;
        //expBar.fillAmount = charSettings.experience;
    }
}
