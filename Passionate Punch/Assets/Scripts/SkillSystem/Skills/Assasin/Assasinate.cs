using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;


namespace Skills
{
    [CreateAssetMenu(menuName = "Scriptables/Skills/Assasinate")]
    public class Assasinate : SkillSettings
    {
        public override void Cast()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator Cooldown(float time)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator ExitCastState(float time)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator RevertSkillEffect(float time)
        {
            throw new System.NotImplementedException();
        }


    }
}
