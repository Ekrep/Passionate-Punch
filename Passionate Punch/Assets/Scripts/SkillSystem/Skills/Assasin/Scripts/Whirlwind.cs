using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using MEC;

public class Whirlwind : MonoBehaviourSkill
{

    //skiller onDestroy oldugunda Datanýn cancast=true
    [SerializeField]
    private ParticleSystem _ps;


    
    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }
    public override void Cast()
    {
        if (skillSettings.canCast)
        {
            GameManager.Instance.SkillCasted(skillSettings.manaCost);
            gameObject.transform.SetPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1f, skillSettings.Character.transform.position.z), Quaternion.identity);
            _ps.GetComponent<ParticleSystem>().Play();
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
            Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
            Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
            skillSettings.canCast = false;
            Hit();
            //destroy ekle

        }


    }

    public override IEnumerator<float> Cooldown(float time)
    {
        
        yield return Timing.WaitForSeconds(time);
        skillSettings.canCast = true;
        

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

    public override IEnumerator<float> ExitCastState(float time)
    {

        yield return Timing.WaitForSeconds(time);
        gameObject.SetActive(false);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

    private void Hit()
    {
       
        Collider[] colliders = new Collider[10];
        int count=0;
        count=Physics.OverlapSphereNonAlloc(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1, skillSettings.Character.transform.position.z), 2, colliders);
        Debug.Log(colliders.Length);
        for (int i = 0; i < count; i++)
        {
            if (colliders[i].TryGetComponent<IHealth>(out IHealth enemyHealth))
            {
                //needs vec3
                enemyHealth.Hit(skillSettings.hitType, skillSettings.skillDamage, colliders[i].gameObject.transform.forward*-1, 350000f);

            }



        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y + 1, skillSettings.Character.transform.position.z), 2);
    }*/
}
