using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketEnter : MonoBehaviour
{
    private void OnEnable()
    {
        BlackMarket.OnBlackMarketEnter += OnMarketEnter;
    }
    private void OnDisable()
    {
        BlackMarket.OnBlackMarketEnter -= OnMarketEnter;
    }
    public void OnMarketEnter()
    {
        UIManager.Instance.interactionButton.gameObject.SetActive(true);
        Debug.Log("Entered to the Black Market");
    }
}
