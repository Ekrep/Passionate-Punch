using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public CharacterBaseStateMachine character;
    private void OnDisable()
    {
        ResetCanCast();
        ResetStackCounts();
    }
    private void Start()
    {
        ResetStackCounts();
    }




    public void ResetCanCast()
    {
        for (int i = 0; i < character.characterSkills.Count; i++)
        {
            character.characterSkills[i].canCast = true;
        }
    }

    public void ResetStackCounts()
    {
        for (int i = 0; i < character.characterSkills.Count; i++)
        {
            character.characterSkills[i].stackCount = character.characterSkills[i].baseStackCount;
        }
    }
}
