using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


public class Invisibility : MonoBehaviourSkill
{
    /// <summary>
    /// Bug cikarsa sharedMat!!
    /// </summary>
    [SerializeField] private Material _firstMat;
    public Material invisMat;
    public GameObject stormExplodePsObject;
    private float _characterFirstMovementSpeed;


    [SerializeField]
    private Material _characterWeaponFirstMat;


    private void OnDestroy()
    {
        skillSettings.canCast = true;
        RevertSkillBuff();
    }

    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            GameManager.Instance.SkillCasted(skillSettings.manaCost);
            _characterFirstMovementSpeed = skillSettings.Character.characterStats.moveSpeed;
            stormExplodePsObject.GetComponent<ParticleSystem>().Play();
            stormExplodePsObject.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.identity);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = invisMat;
            for (int i = 0; i < skillSettings.Character.characterWeapons.Count; i++)
            {
                skillSettings.Character.characterWeapons[i].GetComponent<MeshRenderer>().sharedMaterial = invisMat;
            }
            Timing.RunCoroutine(RevertSkillEffect(skillSettings.activeTime));
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);//Needs animation Adjustment
            Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
            Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
            skillSettings.Character.canVisible = false;
            //BuffAmount
            skillSettings.Character.characterStats.moveSpeed += skillSettings.skillEffectAmount;
            
            skillSettings.canCast = false;



        }

        // Debug.Log(GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material=invisMat);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.Character.canVisible = true;
        RevertSkillBuff();
        if (skillSettings.Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial == invisMat)
        {
            skillSettings.Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = _firstMat;
        }
        for (int i = 0; i < skillSettings.Character.characterWeapons.Count; i++)
        {
            if (skillSettings.Character.characterWeapons[i].GetComponent<MeshRenderer>().sharedMaterial == invisMat)
            {
                skillSettings.Character.characterWeapons[i].GetComponent<MeshRenderer>().sharedMaterial = _characterWeaponFirstMat;
            }
            
        }

    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        yield return Timing.WaitForSeconds(time);
        gameObject.SetActive(false);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
    }

    public override IEnumerator<float> Cooldown(float time)
    {

        yield return Timing.WaitForSeconds(time);
        Debug.Log("cooldownStarted");
        skillSettings.canCast = true;


    }

    private void RevertSkillBuff()
    {
        skillSettings.Character.characterStats.moveSpeed = _characterFirstMovementSpeed;
    }
}
