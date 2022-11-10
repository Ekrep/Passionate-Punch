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
        public override void Cast()
        {
            GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = invisMat;
           // Debug.Log(GameManager.Instance.character.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material=invisMat);
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            throw new System.NotImplementedException();
        }
    }
}
