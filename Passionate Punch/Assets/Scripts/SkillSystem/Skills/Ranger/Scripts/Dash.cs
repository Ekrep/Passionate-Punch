using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Dash : MonoBehaviourSkill
{
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
            GameManager.Instance.SkillCasted(skillSettings.manaCost);
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.transform.rotation = Quaternion.Euler(skillSettings.Character.transform.rotation.x, skillSettings.skillDecalFlag.decalLastRotation.y, skillSettings.Character.transform.rotation.z);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
            Timing.RunCoroutine(MovePlayerToTargetPosition(skillSettings.Character.transform.position));
            skillSettings.canCast = false;
            Timing.RunCoroutine(Cooldown(skillSettings.coolDown));

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
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterMovingState);
        gameObject.SetActive(false);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }
    IEnumerator<float> MovePlayerToTargetPosition(Vector3 playerFirstPos)
    {
        while (Vector3.Distance(skillSettings.Character.transform.position, playerFirstPos + skillSettings.Character.transform.forward * _travelDistance) > 0.1f)
        {

            RaycastHit hit;
            if (Physics.Raycast(skillSettings.Character.transform.position, skillSettings.Character.transform.forward, out hit, Mathf.Infinity) && Vector3.Distance(hit.collider.transform.position, skillSettings.Character.transform.position) < 3f)
            {
                Debug.Log("break");
                break;
            }
            skillSettings.Character.gameObject.transform.position = Vector3.MoveTowards(skillSettings.Character.gameObject.transform.position, playerFirstPos + skillSettings.Character.transform.forward * _travelDistance, _travelSpeed * Time.fixedDeltaTime);
            gameObject.transform.position = skillSettings.Character.transform.position;
            yield return Timing.WaitForOneFrame;



        }
        Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
    }


}
