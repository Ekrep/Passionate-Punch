using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAliveState : CharacterBaseState
{
    protected CharacterBaseStateMachine sm;




    public CharacterAliveState(string name, CharacterBaseStateMachine stateMachine) : base(name, stateMachine)
    {
        sm = (CharacterBaseStateMachine)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }


    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.O) && sm.characterSkills[0].skillType==SkillSystem.SkillSettings.SkillType.Passive)
        {
            sm.characterSkills[0].Cast();
            //sm.characterStats.mana = sm.characterSkills[0].manaCost;
        }
        if (Input.GetKeyDown(KeyCode.P) && sm.characterSkills[1].skillType == SkillSystem.SkillSettings.SkillType.Active)
        {
            sm.characterSkills[1].Cast();
            //sm.characterStats.mana = sm.characterSkills[0].manaCost;
        }
    }







}
