using System;
using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
using Interfaces;
using SkillSystem;

public class HealthBar : MonoBehaviour
{
    public static Action OnPlayerDead;
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
    private void OnEnable()
    {
        EnemyAttackState.OnPlayerTakeDamage += PlayerTakesHit;
    }
    private void OnDisable()
    {
        EnemyAttackState.OnPlayerTakeDamage -= PlayerTakesHit;
    }
    void PlayerTakesHit()
    {
        // Temporary, player takes constant damage, will replace with the actual hit power
        healthBar.value -= 5; // 5 is a constant temporary value
        // Player dies
        if (healthBar.value <= 0) // Will changed with character's health <= 0 ...
        {
            // Listen to this action
            OnPlayerDead?.Invoke();
        }
    }
}
