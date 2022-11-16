using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviourSkill
{
    [SerializeField] private Material _firstMat;
    public Material invisMat;
    public GameObject stormExplodePsObject;



    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }

    public override void Cast()
    {
        if (skillSettings.canCast)
        {

            stormExplodePsObject.GetComponent<ParticleSystem>().Play();
            stormExplodePsObject.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.identity);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = invisMat;
            StartCoroutine(RevertSkillEffect(skillSettings.activeTime));
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);//Needs animation Adjustment
            StartCoroutine(ExitCastState(0.5f));
            StartCoroutine(Cooldown(skillSettings.coolDown));
            skillSettings.canCast = false;
        }

        // Debug.Log(GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material=invisMat);
    }

    public override IEnumerator RevertSkillEffect(float time)
    {
        yield return new WaitForSeconds(time);
        skillSettings.Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _firstMat;
    }

    public override IEnumerator ExitCastState(float time)
    {
        yield return new WaitForSeconds(time);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
    }

    public override IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        skillSettings.canCast = true;

        DestroyImmediate(gameObject);
    }
}
