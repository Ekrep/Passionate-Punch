using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class AttackSpeedBoost : MonoBehaviourSkill
{
    
    public List<ParticleSystem> particles;

    [SerializeField]
    private ParticleSystem _boostIcon;

    private float _characterFirstAttackSpeed;

    private void OnDestroy()
    {
        skillSettings.canCast = true;
        
    }
    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            GameManager.Instance.SkillCasted(skillSettings.manaCost);
            _characterFirstAttackSpeed = skillSettings.Character.characterStats.attackSpeed;
            skillSettings.Character.anim.SetFloat("AttackSpeed", _characterFirstAttackSpeed + skillSettings.skillEffectAmount);
            Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
            Timing.RunCoroutine(RevertSkillEffect(skillSettings.activeTime));
            SetParticles();
            SetBoostIcon();
            skillSettings.canCast = false;
            


        }
    }

    public override IEnumerator<float> Cooldown(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.canCast = true;
        
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.Character.anim.SetFloat("AttackSpeed", _characterFirstAttackSpeed);
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
            particles[i].transform.SetParent(this.gameObject.transform);
            particles[i].transform.position = Vector3.zero;
        }
        _boostIcon.transform.SetParent(this.gameObject.transform);
        gameObject.SetActive(false);
    }

    private void SetParticles()
    {
        particles[0].transform.SetParent(skillSettings.Character.characterWeapons[0].transform);
        particles[0].transform.localPosition = Vector3.zero;
        particles[0].Play();

        particles[1].transform.SetParent(skillSettings.Character.characterWeapons[1].transform);
        particles[1].transform.localPosition = Vector3.zero;
        particles[1].Play();
    }

    private void SetBoostIcon()
    {
        _boostIcon.transform.SetParent(skillSettings.Character.transform);
        _boostIcon.transform.position = new Vector3(skillSettings.Character.transform.position.x+1, skillSettings.Character.transform.position.y + 1, skillSettings.Character.transform.position.z);
        _boostIcon.Play();
    }
}
