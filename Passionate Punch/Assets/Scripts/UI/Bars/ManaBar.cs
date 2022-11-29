using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    CharacterSettings charSettings;
    GameManager gameManager;
    Slider manaBar;
    UIManager UImanager;
    private void Start()
    {
        UImanager = UIManager.Instance;
        gameManager = GameManager.Instance;
        charSettings = gameManager.character.characterStats;
        manaBar = GetComponent<Slider>();
        // Update the mana bar values wheter player is an assassin or a ranger
        // Player max mana
        manaBar.maxValue = charSettings.maxMana;
        // Player mana value
        manaBar.value = charSettings.maxMana; // Just for the start
        charSettings.mana = charSettings.maxMana; // Just for the start
    }
    public void CheckManaForInvisible()
    {
        // 10 is equal to the mana cost of this skill
        if (charSettings.mana >= 10)
        {
            Debug.Log("Invisible skill casted");
            charSettings.mana -= 10; // This will change
            manaBar.value -= 10;
            //UImanager.castableSkillInvis = true;
        }
        else
        {
            Debug.Log("Unable to cast this skill");
            //UImanager.castableSkillInvis = false;
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
            //UImanager.castableSkillWhirl = true;
        }
        else
        {
            Debug.Log("Unable to cast this skill");
            //UImanager.castableSkillWhirl = false;
        }
    }
}
