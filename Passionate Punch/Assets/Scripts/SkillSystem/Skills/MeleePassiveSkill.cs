using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

namespace Skills
{

    [CreateAssetMenu(menuName = "Scriptables/Skills/MeleePassiveSkill")]

    public class MeleePassiveSkill : SkillSettings
    {
        public override void Cast()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            throw new System.NotImplementedException();
        }
    }
}
