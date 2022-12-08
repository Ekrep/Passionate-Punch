using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;
using SkillSystem;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private List<TreeSlot> skillTreeSlots;
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
            skillTreeSlots[i].slotImage.sprite = _Character.characterSkills[i].skillSprite;
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
    public void SetSkillSelection(TreeSlot slot)
    {
        if (skillSelectionUI.activeSelf)
        {
            skillSelectionUI.SetActive(false);
        }
        else
        {
            skillSelectionUI.SetActive(true);
        }

        firstSkillButton.onClick.AddListener(() => ChangeSkill(slot.slotIndex, 0));
        secondSkillButton.onClick.AddListener(() => ChangeSkill(slot.slotIndex, 1));
    }

    public void ChangeSkill(int skillIndex, int slotIndex)
    {
        SkillSettings temp = _Character.characterSkills[slotIndex];
        _Character.characterSkills[slotIndex] = _Character.characterSkills[skillIndex];
        _Character.characterSkills[skillIndex] = temp;
        skillIndex = slotIndex;
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