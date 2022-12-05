using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private List<Image> skillTreeSlots;
    [SerializeField] private List<Image> skillButtons;

    [SerializeField] private CharacterSettings _Character;


     void GameManager_OnSendCharacter(CharacterBaseStateMachine obj){
            _Character = obj.characterStats;
            for(int i = 0; i < skillTreeSlots.Count; i++)
            {
                skillTreeSlots[i].sprite = _Character.skillList[i].skillSprite;
            }

            for(int i = 0; i < skillButtons.Count; i++)
            {
                skillButtons[i].GetComponentInChildren<Image>().sprite = skillTreeSlots[i].sprite;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
