using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIManager : MonoBehaviour
{
    public ScriptableBool blackMarket;
    public GameObject blackMarketPanel;
    private void OnEnable()
    {
        DialogueManager.SuccessfulSpeak += OpenBlackMarket;
    }
    private void OnDisable()
    {
        DialogueManager.SuccessfulSpeak -= OpenBlackMarket;
    }

    void OpenBlackMarket()
    {
        if (blackMarket.value)
        {
            // If player talks with the black market, black market panel would be open
            blackMarketPanel.gameObject.SetActive(true);
        }
    }
}
