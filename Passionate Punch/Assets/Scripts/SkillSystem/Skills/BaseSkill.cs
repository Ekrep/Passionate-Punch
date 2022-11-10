using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

namespace Skills
{
    [CreateAssetMenu(menuName = "Scriptables/Skills/BaseSkill")]


    public class BaseSkill : SkillSettings
    {
        public Material invisMat;
        public GameObject stormExplodePsObject;
        
        public override void Cast()
        {
            GameObject gameObject;
            gameObject=Instantiate(stormExplodePsObject);
            gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.transform.SetPositionAndRotation(new Vector3(Character.transform.position.x,Character.transform.position.y+1f,Character.transform.position.z), Quaternion.identity);
            Character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = invisMat;

            Destroy(gameObject, 1f);
           // Debug.Log(GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material=invisMat);
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            throw new System.NotImplementedException();
        }


        


        public CharacterStateMachine Character
        {
            get
            {
                return GameManager.Instance.character;
            }
        }
    }
}
