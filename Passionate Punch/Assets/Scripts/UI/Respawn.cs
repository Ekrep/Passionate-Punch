using System;
using System.Collections;
using System.Collections.Generic;
using CharacterSystem;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static event Action OnRespawn;
    public GameObject respawnPanel;
    Animator respawnPanelAnimator;
    private void Start()
    {
        respawnPanel.gameObject.SetActive(false);
        respawnPanelAnimator = respawnPanel.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        CharacterHealth.OnPlayerDeath += ActivatePanel;
    }
    private void OnDisable()
    {
        CharacterHealth.OnPlayerDeath -= ActivatePanel;
    }
    // Activates the game over panel
    void ActivatePanel()
    {
        respawnPanel.gameObject.SetActive(true);
    }
    public void Respawning()
    {
        respawnPanelAnimator.SetTrigger("Pressed");
        OnRespawn?.Invoke();
    }
}
