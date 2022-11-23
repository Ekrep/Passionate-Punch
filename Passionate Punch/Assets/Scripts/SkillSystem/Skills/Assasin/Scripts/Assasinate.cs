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
        if (skillSettings.canCast)
        {
            skillSettings.Character.TryGetComponent<AutoAim>(out AutoAim aim);
            if (aim.targetEnemy != null)
            {
                Debug.Log("girdim");
                skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
                //Needs Fix Later
                skillSettings.Character.transform.position = new Vector3(aim.targetEnemy.position.x, aim.targetEnemy.position.y, aim.targetEnemy.position.z - 1f);
                aim.focusedEnemy.Hit(SkillSystem.SkillSettings.HitType.Low, skillSettings.skillPureDamage, Vector3.zero, 0);
                Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
                skillSettings.canCast = false;
            }
        }
    }

    public override IEnumerator<float> Cooldown(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }


}
