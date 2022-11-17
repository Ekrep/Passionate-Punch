using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [HideInInspector]
    public AudioSource audioSource;
    [Header("BlackMarket")]
    public AudioClip[] BlackMarketSFX;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
}
