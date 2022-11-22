using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanSkillCastableState : CharacterAliveState
{
    public CharacterCanSkillCastableState(string name, CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {

    }

    public override void Update()
    {
        base.Update();

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                if (UIManager.Instance.isPressedSkillOne && sm.characterSkills[0].skillType == SkillSystem.SkillSettings.SkillType.Passive)
                {
                    sm.characterSkills[0].Cast();
                    //sm.characterStats.mana = sm.characterSkills[0].manaCost;
                }
                if (UIManager.Instance.isPressedSkillTwo && sm.characterSkills[1].skillType == SkillSystem.SkillSettings.SkillType.Active)
                {
                    sm.characterSkills[1].Cast();
                    //sm.characterStats.mana = sm.characterSkills[0].manaCost;
                }
                break;

            case RuntimePlatform.WindowsEditor:
                if (Input.GetKeyDown(KeyCode.O) /*&& sm.characterSkills[0].skillType == SkillSystem.SkillSettings.SkillType.Passive*/)
                {
                    sm.characterSkills[0].Cast();
                    //sm.characterStats.mana = sm.characterSkills[0].manaCost;
                }
                if (Input.GetKeyDown(KeyCode.P) /*&& sm.characterSkills[1].skillType == SkillSystem.SkillSettings.SkillType.Active*/)
                {
                    sm.characterSkills[1].Cast();
                    //sm.characterStats.mana = sm.characterSkills[0].manaCost;
                }
                if (Input.GetKeyDown(KeyCode.I) /*&& sm.characterSkills[1].skillType == SkillSystem.SkillSettings.SkillType.Active*/)
                {
                    sm.characterSkills[2].Cast();
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    sm.characterSkills[3].Cast();
                }
                break;
        }
        if (UIManager.Instance.isPressedSkillOne && sm.characterSkills[0].skillType == SkillSystem.SkillSettings.SkillType.Passive)
        {
            sm.characterSkills[0].Cast();
            //sm.characterStats.mana = sm.characterSkills[0].manaCost;
        }
        if (UIManager.Instance.isPressedSkillTwo && sm.characterSkills[1].skillType == SkillSystem.SkillSettings.SkillType.Active)
        {
            sm.characterSkills[1].Cast();
            //sm.characterStats.mana = sm.characterSkills[0].manaCost;
        }
    }
}
