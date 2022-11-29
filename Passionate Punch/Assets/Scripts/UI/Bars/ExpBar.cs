using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;   

public class ExpBar : MonoBehaviour
{
    CharacterSettings charSettings;
    GameManager gameManager;
    Slider expBar;
    private void Start()
    {
        gameManager = GameManager.Instance;
        charSettings = gameManager.character.characterStats;
        expBar = GetComponent<Slider>();
        // Update the experience bar values wheter player is an assassin or a ranger
        // Set the max experience point to reach next level.
        expBar.value = charSettings.experience; // Temporary value
    }
}
