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

    [SerializeField] private CharacterSettings _Character;
    [SerializeField] private GameObject skillSelectionUI;


    void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        _Character = obj.characterStats;
        for (int i = 0; i < skillTreeSlots.Count; i++)
        {
            skillTreeSlots[i].sprite = _Character.skillList[i].skillSprite;
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

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateSkillButtons()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            skillButtons[i].GetComponentInChildren<Image>().sprite = _Character.skillList[i].skillSprite;
        }
    }

    public void ChangeSkill(int index)
    {
            SkillSettings temp = _Character.skillList[0];
            _Character.skillList[0] = _Character.skillList[index];
            _Character.skillList[index] = temp;
            UpdateSkillButtons();
    }



    public void SetSkillSelection()
    {
        if (skillSelectionUI.activeSelf)
        {
            skillSelectionUI.SetActive(false);
        }
        else
        {
            skillSelectionUI.SetActive(true);
        }
    }
}
