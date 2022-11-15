using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketExit : MonoBehaviour
{
    private void OnEnable()
    {
        BlackMarket.OnBlackMarketExit += OnMarketExit;
    }
    private void OnDisable()
    {
        BlackMarket.OnBlackMarketExit -= OnMarketExit;
    }
    public void OnMarketExit()
    {
        UIManager.Instance.interactionButton.interactable = false;
        Debug.Log("Exited to the Black Market");
    }
}
