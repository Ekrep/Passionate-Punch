using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

namespace Skills
{
    [CreateAssetMenu(menuName = "Scriptables/Skills/BaseSkill")]


    public class BaseSkill : SkillSettings
    {
        private Material firstMat;
        public Material invisMat;
        public GameObject stormExplodePsObject;
        
        public override void Cast()
        {
            GameObject gameObject;
            gameObject=Instantiate(stormExplodePsObject);
            gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.transform.SetPositionAndRotation(new Vector3(Character.transform.position.x,Character.transform.position.y+1f,Character.transform.position.z), Quaternion.identity);
            firstMat = Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
            Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = invisMat;
            Character.StartCoroutine(RevertSkillEffect(activeTime));
            Character.ChangeState(Character.characterSkillCastState);
            Character.anim.SetBool(animationName, true);//Needs animation Adjustment
            Character.StartCoroutine(ExitCastState(0.5f));

            Destroy(gameObject, 1f);
           // Debug.Log(GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material=invisMat);
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            yield return new WaitForSeconds(time);
            Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = firstMat;
        }

        public override IEnumerator ExitCastState(float time)
        {
            yield return new WaitForSeconds(time);
            Character.anim.SetBool(animationName, false);
            Character.ChangeState(Character.characterIdleState);
        }

        


       
    }
}
