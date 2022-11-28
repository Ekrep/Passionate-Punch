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
        manaBar.value = assassin.mana;
    }
}
