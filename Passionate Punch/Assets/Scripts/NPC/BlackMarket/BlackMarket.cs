using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarket : MonoBehaviour
{
    public static Action OnBlackMarketEnter, OnBlackMarketExit;
    // When player enters or exits from the trigger area of the black market, actions are triggered.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnBlackMarketEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnBlackMarketExit?.Invoke();
        }
    }
}
