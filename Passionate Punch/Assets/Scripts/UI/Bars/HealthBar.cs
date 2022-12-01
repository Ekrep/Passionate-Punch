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
    CharacterBaseStateMachine character;
    GameManager gameManager;
    Slider healthBar;

    bool isRegenerating;
    private void Start()
    {

        healthBar = GetComponent<Slider>();
        // Change the health bar value wheter the player is an assassin or ranger.
        // Players max health
        healthBar.maxValue = charSettings.maxHealth;
        // Players health
        healthBar.value = charSettings.maxHealth;  // Just for the start
    }
    private void OnEnable()
    {
        CharacterHealth.OnTakeDamage += PlayerTakesHit;
        CharacterHealth.OnHealthRecovery += RegenerateHealth;
        GameManager.OnSendCharacter += PullChar;
    }
    private void OnDisable()
    {
        CharacterHealth.OnTakeDamage -= PlayerTakesHit;
        CharacterHealth.OnHealthRecovery -= RegenerateHealth;
        GameManager.OnSendCharacter -= PullChar;
    }
    void PlayerTakesHit(float health)
    {
        Debug.Log("Health:  " + health);
        // Temporary, player takes constant damage, will replace with the actual hit power
        healthBar.value = health; 
        // Player dies
        if (health <= 0)
        {
            // Listen to this action
            OnPlayerDead?.Invoke();
        }
    }
    void RegenerateHealth(float health)
    {
        Debug.Log("Health regenerating => " + health);
        healthBar.value += charSettings.healthRecoveryAmount;
    }
    void PullChar(CharacterBaseStateMachine characterBaseStateMachine)
    {
        charSettings = characterBaseStateMachine.characterStats;
        character = characterBaseStateMachine;
        Debug.Log("Character pulled succesfully");
    }
}
