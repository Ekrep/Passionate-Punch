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
        GameManager.OnSkillCasted += CheckManaForSkills;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
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
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
        Debug.Log("Character pulled succesfully");
    }
    /* Old 
    public void CheckManaForInvisible(float manaCost)
    {
        Debug.Log("Mana cost is => " + manaCost);
        // 10 is equal to the mana cost of this skill
        if (charSettings.mana >= 10)
        {
            Debug.Log("Invisible skill casted");
            charSettings.mana -= 10; // This will change
            manaBar.value -= 10;
        }
        else
        {
            Debug.Log("Unable to cast this skill");
        }
    }
    public void CheckManaForWhirl()
    {
        // 10 is equal to the mana cost of this skill
        if (charSettings.mana >= 30)
        {
            Debug.Log("Whirl skill casted");
            charSettings.mana -= 30;
            manaBar.value -= 30;
        }
        else
        {
            Debug.Log("Unable to cast this skill");
        }
    }
    */
}
