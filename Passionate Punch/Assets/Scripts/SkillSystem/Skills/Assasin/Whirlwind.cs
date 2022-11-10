using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;


namespace Skills
{
    [CreateAssetMenu(menuName = "Scriptables/Skills/WhrilWind")]
    public class Whirlwind : SkillSettings
    {

        public GameObject particleSystemGameObjectPrefab;
        public override void Cast()
        {
            GameObject gO;
            gO = Instantiate(particleSystemGameObjectPrefab);
            gO.transform.SetPositionAndRotation(new Vector3(Character.transform.position.x, Character.transform.position.y + 1f, Character.transform.position.z), Quaternion.Euler(-90,0,0));
            gO.GetComponent<ParticleSystem>().Play();
            Character.ChangeState(Character.characterSkillCastState);
            Character.anim.SetBool(animationName, true);

            Character.StartCoroutine(ExitCastState(0.7f));
            Destroy(gO, 0.5f);

        }

        public override IEnumerator ExitCastState(float time)
        {
            yield return new WaitForSeconds(time);
            Character.anim.SetBool(animationName, false);
            Character.ChangeState(Character.characterIdleState);
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            throw new System.NotImplementedException();
        }



    }
}

