using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;
using SkillSystem;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private List<Image> skillTreeSlots;
    [SerializeField] private List<Image> skillButtons;

    [SerializeField] private CharacterSettings _CharacterStats;
    [SerializeField] private CharacterBaseStateMachine _Character;

    [SerializeField] private GameObject skillSelectionUI;

    [SerializeField] private Button firstSkillButton;
    [SerializeField] private Button secondSkillButton;


    void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        _Character = obj;
        _CharacterStats = obj.characterStats;
        for (int i = 0; i < skillTreeSlots.Count; i++)
        {
            skillTreeSlots[i].sprite = _Character.characterSkills[i].skillSprite;
        }
    }
    void OnEnable()
    {
        GameManager.OnSendCharacter += GameManager_OnSendCharacter;
    }

    void OnDisable()
    {
        GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
    }

    public void UpdateSkillButtons()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            skillButtons[i].GetComponentInChildren<Image>().sprite = _Character.characterSkills[i].skillSprite;
        }
    }

    public void SetSkillSelection(int index)
    {
        if (skillSelectionUI.activeSelf)
        {
            skillSelectionUI.SetActive(false);
        }
        else
        {
            skillSelectionUI.SetActive(true);
        }

        firstSkillButton.onClick.AddListener(() => ChangeFirstSkill(index));
        secondSkillButton.onClick.AddListener(() => ChangeSecondSkill(index));
    }

    public void ChangeFirstSkill(int index)
    {
        if(index == 0)
        {
            return;
        }
        SkillSettings temp = _Character.characterSkills[0];
        _Character.characterSkills[0] = _Character.characterSkills[index];
        _Character.characterSkills[index] = temp;
        UpdateSkillButtons();
        skillSelectionUI.SetActive(false);
    }

    public void ChangeSecondSkill(int index)
    {
        if(index == 1)
        {
            return;
        }
        SkillSettings temp = _Character.characterSkills[1];
        _Character.characterSkills[1] = _Character.characterSkills[index];
        _Character.characterSkills[index] = temp;
        UpdateSkillButtons();
        skillSelectionUI.SetActive(false);
    }
}