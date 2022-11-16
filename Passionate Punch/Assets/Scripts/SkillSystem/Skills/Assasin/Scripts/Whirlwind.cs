using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviourSkill
{

    //skiller onDestroy oldugunda Datanýn can cast=true
    public GameObject particleSystemGameObjectPrefab;
    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            GameObject gO;
            gO = Instantiate(particleSystemGameObjectPrefab);
            gO.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.Euler(-90, 0, 0));
            gO.GetComponent<ParticleSystem>().Play();
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);

            StartCoroutine(ExitCastState(0.7f));
            StartCoroutine(Cooldown(skillSettings.coolDown));
            Destroy(gO, 0.5f);

        }


    }

    public override IEnumerator Cooldown(float time)
    {
        Debug.Log("whrilWind");
        skillSettings.canCast = false;
        if (skillSettings.stackCount > 0)
        {
            skillSettings.stackCount--;
        }
        yield return new WaitForSeconds(skillSettings.stackCastCoolDown);
        skillSettings.canCast = true;
        if (skillSettings.stackCount == 0)
        {
            skillSettings.canCast = false;
        }
        yield return new WaitForSeconds(time);
        skillSettings.stackCount++;
        if (skillSettings.stackCount > 0)
        {
            skillSettings.canCast = true;
        }
    }

    public override IEnumerator ExitCastState(float time)
    {
        yield return new WaitForSeconds(time);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
    }

    public override IEnumerator RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }
}
