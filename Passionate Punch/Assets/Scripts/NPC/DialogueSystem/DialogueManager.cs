using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] Animator dialoguePanelAnimator;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choiceTexts;

    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }
    bool isAtChoice;

    static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one Dialogue Manager in the scene");
        }
        instance = this;
    }
    private void Start()
    {
        isAtChoice = false;
        //dialoguePanel.gameObject.SetActive(false);
        isDialoguePlaying = false;
        // Get all of the choices texts
        choiceTexts = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choiceTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    private void Update()
    {
        // Return right away if dialogue isn't playing 
        if (!isDialoguePlaying)
        {
            return;
        }
        // Handle continuing to the next line in the dialogue when submit is pressed
        if (Input.GetMouseButtonDown(0) && !isAtChoice)
        {
            ContinueStory();
        }
    }
    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        // If there is a choice in the current context, choice bool is set to true because to prevent the little bug.
        if (currentChoices.Count > 0)
        {
            isAtChoice = true;
        }
        else
        {
            isAtChoice = false;
        }
        // Defensice check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices has given than the UI can support. Number of choices given: " + currentChoices.Count);
        }
        int index = 0;
        // Enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choiceTexts[index].text = choice.text;
            index++;
        }
        // Go through the remining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        //dialoguePanel.gameObject.SetActive(true);
        dialoguePanelAnimator.SetBool("IsOpen", true);
        ContinueStory();
    }
    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // Display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(.2f);
        isDialoguePlaying = false;
        //dialoguePanel.gameObject.SetActive(false);
        dialoguePanelAnimator.SetBool("IsOpen", false);
        dialogueText.text = "";
        // Make available the interaction button again
        UIManager.Instance.interactionButton.interactable = true;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
