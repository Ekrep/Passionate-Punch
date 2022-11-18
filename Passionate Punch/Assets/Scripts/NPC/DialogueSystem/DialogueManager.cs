using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    Queue<string> sentences;
    List<bool> selectives;
    public ScriptableBool isInDialogue;
    public TextMeshProUGUI nameText, dialogueText, choosingButtonText_1, choosingButtonText_2;
    public Animator dialogueAnimator;
    public Button choosingButton_1, choosingButton_2;

    void Start()
    {
        sentences = new Queue<string>();
        selectives = new List<bool>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInDialogue.value)
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        dialogueAnimator.SetBool("IsOpen", true);
        sentences.Clear();
        selectives.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (bool selective in dialogue.isSelective)
        {
            selectives.Add(selective);
        }
        DisplayNextSentence();
    }
    public void DisplaySelectiveButtons()
    {
        if (selectives[Dialogue.index])
        {
            string sentenceOfOurs_1 = sentences.Dequeue();
            choosingButton_1.gameObject.SetActive(true);
            choosingButtonText_1.text = sentenceOfOurs_1;
            choosingButton_2.gameObject.SetActive(true);
            string sentenceOfOurs_2 = sentences.Dequeue();
            choosingButtonText_2.text = sentenceOfOurs_2;
        }
        else
        {
            choosingButton_1.gameObject.SetActive(false);
            choosingButton_2.gameObject.SetActive(false);
        }
        Dialogue.index++;
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        DisplaySelectiveButtons();
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; 
            yield return null;
        }
    }
    void EndDialogue()
    {
        // SFX Manager
        SFXManager.instance.audioSource.clip = SFXManager.instance.BlackMarketSFX[1];
        SFXManager.instance.audioSource.Play();
        /////////////////////////////////////
        dialogueAnimator.SetBool("IsOpen", false);
        // When conversation ends, interaction button activates again
        UIManager.Instance.interactionButton.interactable = true;
        Dialogue.index = 0;
    }
}
