using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public CharacterSettings assassin, ranger;
    Slider healthBar;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        // Change the health bar value wheter the player is an assassin or ranger.
        // Players max health
        healthBar.maxValue = assassin.maxHealth;
        // Players health
        healthBar.value = 10;  // Temporary value
    }
}
