using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Interfaces;

public class Assasinate : MonoBehaviourSkill
{
    private void OnDestroy()
    {
        
    }

    public override void Cast()
    {
        skillSettings.Character.TryGetComponent<AutoAim>(out AutoAim aim);
       
        skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
        if (aim!=null)
        {
           
            skillSettings.Character.transform.position = new Vector3(aim.targetEnemy.position.x, aim.targetEnemy.position.y, aim.targetEnemy.position.z-1f);
        }
      
    }

    public override IEnumerator<float> Cooldown(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

   
}
