using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.Rendering.Universal;//for decal


public class GunStorm : MonoBehaviourSkill
{
    
    private bool isExitState;

    [SerializeField]
    private float _travelDistance;

    [SerializeField]
    private float _travelSpeed;

    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }
    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            isExitState = false;
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.transform.rotation = Quaternion.Euler(skillSettings.Character.transform.rotation.x, skillSettings.skillDecalFlag.decalLastRotation.y, skillSettings.Character.transform.rotation.z);
            Timing.RunCoroutine(MovePlayerToTargetPosition(skillSettings.Character.transform.position));
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
            Timing.RunCoroutine(PlayGunParticleEffects());
            //gameObject.GetComponent<DecalProjector>().size-> Decal!!
           
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
        isExitState = true;
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterMovingState);
        gameObject.SetActive(false);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator<float> PlayGunParticleEffects()
    {
        while (!isExitState)
        {
            skillSettings.Character.characterWeaponsParticle[0].Play();
            yield return Timing.WaitForSeconds(0.2f);
            skillSettings.Character.characterWeaponsParticle[1].Play();
        }
       

    }
    IEnumerator<float> MovePlayerToTargetPosition(Vector3 playerFirstPos)
    {
        while (Vector3.Distance(skillSettings.Character.transform.position,playerFirstPos+skillSettings.Character.transform.forward*_travelDistance)>0.1f)
        {

            skillSettings.Character.gameObject.transform.position = Vector3.MoveTowards(skillSettings.Character.gameObject.transform.position, playerFirstPos + skillSettings.Character.transform.forward * _travelDistance, _travelSpeed*Time.fixedDeltaTime);
            yield return Timing.WaitForOneFrame;

        }
        Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
    }
  
}
