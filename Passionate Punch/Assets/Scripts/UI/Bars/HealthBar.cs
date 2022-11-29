using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    CharacterSettings charSettings;
    GameManager gameManager;
    Slider healthBar;
    private void Start()
    {
        gameManager = GameManager.Instance;
        charSettings = gameManager.character.characterStats;
        healthBar = GetComponent<Slider>();
        // Change the health bar value wheter the player is an assassin or ranger.
        // Players max health
        healthBar.maxValue = charSettings.maxHealth;
        // Players health
        healthBar.value = charSettings.maxHealth;  // Just for the start
    }
}
