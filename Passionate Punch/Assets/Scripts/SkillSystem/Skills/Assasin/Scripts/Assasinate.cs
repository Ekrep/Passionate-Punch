using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Interfaces;

public class Assasinate : MonoBehaviourSkill
{

    [SerializeField]
    private ParticleSystem _slashParticle;

    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }

    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            skillSettings.Character.TryGetComponent<AutoAim>(out AutoAim aim);
            if (aim.targetEnemy != null)
            {
                
                SetRotationToEnemy(aim.targetEnemy, skillSettings.Character.gameObject.transform);
                skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
                skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
                skillSettings.Character.transform.position =aim.targetEnemy.transform.position+aim.targetEnemy.forward*-1f;
                aim.focusedEnemy.Hit(SkillSystem.SkillSettings.HitType.Low, skillSettings.skillDamage, Vector3.zero, 0);
                _slashParticle.transform.position = new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z);
                SetRotationToEnemy(aim.targetEnemy, _slashParticle.transform);
                _slashParticle.Play();
                Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
                Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
                skillSettings.canCast = false;
            }
        }
    }

    public override IEnumerator<float> Cooldown(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.canCast = true;
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        gameObject.SetActive(false);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

    private void SetRotationToEnemy(Transform targetEnemy,Transform objectTransform)
    {
        //Needs adjustment on Rotation Calculation(-?,+?)
        Vector3 deltaPos = Vector3.zero;
        deltaPos = objectTransform.position - targetEnemy.position;
        float target = Mathf.Atan2(-deltaPos.x, -deltaPos.z) * Mathf.Rad2Deg;
        objectTransform.rotation = Quaternion.Euler(skillSettings.Character.gameObject.transform.rotation.x, -target, skillSettings.Character.gameObject.transform.rotation.z);
    }

}
