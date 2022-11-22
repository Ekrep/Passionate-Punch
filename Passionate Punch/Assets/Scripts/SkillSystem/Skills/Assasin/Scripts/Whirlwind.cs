using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Whirlwind : MonoBehaviourSkill
{

    //skiller onDestroy oldugunda Datanýn cancast=true
    [SerializeField]
    private ParticleSystem _ps;
    [SerializeField]


    private void OnDisable()
    {
        skillSettings.canCast = true;

    }
    public override void Cast()
    {
        if (skillSettings.canCast)
        {

            gameObject.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.identity);
            _ps.GetComponent<ParticleSystem>().Play();
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
            StartCoroutine(ExitCastState(0.7f));
            StartCoroutine(Cooldown(skillSettings.coolDown));
            skillSettings.canCast = false;
            Hit();
            //destroy ekle

        }


    }

    public override IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        skillSettings.canCast = true;
        gameObject.SetActive(false);

        /*if (skillSettings.stackCount > 0)
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
        }*/
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

    private void Hit()
    {
        skillSettings.Character.TryGetComponent<IHealth>(out IHealth playerHealth);
        Collider[] colliders = new Collider[10];
        Physics.OverlapSphereNonAlloc(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1, skillSettings.Character.transform.position.z), 2, colliders);
        Debug.Log(colliders.Length);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].TryGetComponent<IHealth>(out IHealth enemyHealth) && enemyHealth != playerHealth)
            {
                Debug.Log(enemyHealth);

            }



        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1, skillSettings.Character.transform.position.z), 2);
    }
}
