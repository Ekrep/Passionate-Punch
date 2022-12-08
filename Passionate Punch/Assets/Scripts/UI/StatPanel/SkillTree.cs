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

        for(int i = 0; i < _Character.characterSkills.Count; i++)
        {
            _Character.characterSkills[i].skillIndex = i;
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

        firstSkillButton.onClick.AddListener(() => ChangeSkill(index, 0));
        secondSkillButton.onClick.AddListener(() => ChangeSkill(index, 1));
    }

    public void ChangeSkill(int skillIndex, int slotIndex)
    {
        Debug.Log(skillIndex);
        if(skillIndex == 0 || skillIndex == 1)
        {
            return;
        }
        SkillSettings temp = _Character.characterSkills[slotIndex];
        _Character.characterSkills[slotIndex] = _Character.characterSkills[skillIndex];
        _Character.characterSkills[skillIndex] = temp;
        UpdateSkillIndexes();
        UpdateSkillButtons();
        skillSelectionUI.SetActive(false);
    }

    public void UpdateSkillIndexes()
    {
        for(int i = 0; i < _Character.characterSkills.Count; i++)
        {
            _Character.characterSkills[i].skillIndex = i;
        }
    }
}