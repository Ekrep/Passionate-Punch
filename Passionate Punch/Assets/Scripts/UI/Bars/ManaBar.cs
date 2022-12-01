using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    CharacterSettings charSettings;
    Slider manaBar;
    UIManager UImanager;
    private void OnEnable()
    {
        CharacterMana.OnManaRecoveryEnabled += RegenerateMana;
        GameManager.OnSkillCasted += CheckManaForSkills;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
        CharacterMana.OnManaRecoveryEnabled -= RegenerateMana;
        GameManager.OnSkillCasted -= CheckManaForSkills;
        GameManager.OnSendCharacter -= PullChar;
    }
    private void Start()
    {
        UImanager = UIManager.Instance;
        manaBar = GetComponent<Slider>();
        // Update the mana bar values wheter player is an assassin or a ranger
        // Player max mana
        manaBar.maxValue = charSettings.maxMana;
        // Player mana value
        manaBar.value = charSettings.maxMana; // Just for the start
        charSettings.mana = charSettings.maxMana; // Just for the start
    }
    public void CheckManaForSkills(float manaCost)
    {
        if (charSettings.mana >= manaCost)
        {
            Debug.Log("Skill casted, mana cost => " + manaCost);
            charSettings.mana -= manaCost; // This will change (left side of the equalization)
            manaBar.value -= manaCost;
        }
        else
        {
            Debug.LogError("Unable to cast this skill, mana is not enough");
        }
    }
    void RegenerateMana()
    {
        Debug.Log("Mana regenerating: " + charSettings.mana);
        manaBar.value += charSettings.manaRecoveryAmount;
    }
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
    }
}
