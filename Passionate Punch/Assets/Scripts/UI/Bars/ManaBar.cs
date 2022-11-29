using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    Slider manaBar;
    public CharacterSettings assassin, ranger;
    private void Start()
    {
        manaBar = GetComponent<Slider>();
        // Update the mana bar values wheter player is an assassin or a ranger
        // Player max mana
        manaBar.maxValue = assassin.maxMana;
        // Player mana value
        manaBar.value = assassin.maxMana; // Just for the start
        assassin.mana = assassin.maxMana; // Just for the start
    }
    public void CheckManaForInvisible()
    {
        // 10 is equal to the mana cost of this skill
        if (assassin.mana >= 10)
        {
            Debug.Log("Invisible skill casted");
            assassin.mana -= 10;
            manaBar.value -= 10;
        }
    }
    public void CheckManaForWhirl()
    {
        // 10 is equal to the mana cost of this skill
        if (assassin.mana >= 30)
        {
            Debug.Log("Whirl skill casted");
            assassin.mana -= 30;
            manaBar.value -= 30;
        }
    }
}
