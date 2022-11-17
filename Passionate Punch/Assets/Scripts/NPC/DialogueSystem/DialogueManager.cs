using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    Queue<string> sentences;
    public ScriptableBool isInDialogue;
    public TextMeshProUGUI nameText, dialogueText;
    public Animator dialogueAnimator;

    void Start()
    {
        sentences = new Queue<string>();
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
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
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
        dialogueAnimator.SetBool("IsOpen", false);
        // When conversation ends, interaction button activates again
        UIManager.Instance.interactionButton.interactable = true;
    }
}
