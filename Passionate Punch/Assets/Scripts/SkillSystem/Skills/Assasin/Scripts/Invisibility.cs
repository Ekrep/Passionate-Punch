using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviourSkill
{
    /// <summary>
    /// Bug cikarsa sharedMat!!
    /// </summary>
    [SerializeField] private Material _firstMat;
    public Material invisMat;
    public GameObject stormExplodePsObject;



    private void OnDisable()
    {
        skillSettings.canCast = true;
    }

    public override void Cast()
    {
        if (skillSettings.canCast)
        {

            stormExplodePsObject.GetComponent<ParticleSystem>().Play();
            stormExplodePsObject.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.identity);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = invisMat;
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
        if (skillSettings.Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial==invisMat)
        {
            skillSettings.Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = _firstMat;
        }
        
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

        gameObject.SetActive(false);
    }
}
