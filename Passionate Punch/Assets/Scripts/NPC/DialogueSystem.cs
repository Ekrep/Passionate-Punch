using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button InteractionButton;
    public string[] dialogue;
    public float wordSpeed;
    int index;
    private void OnEnable()
    {
        BlackMarket.OnBlackMarketExit += zeroText;
        InteractionButton.onClick.AddListener(OnInteract);
    }
    private void OnDisable()
    {
        BlackMarket.OnBlackMarketExit -= zeroText;
    }
    private void Update()
    {
        // For next line
        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }
    public void OnInteract()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
}
