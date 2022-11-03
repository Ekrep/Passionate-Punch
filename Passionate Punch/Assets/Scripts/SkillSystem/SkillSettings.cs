using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SkillSettings : ScriptableObject
    {
        public string skillName;
        public Sprite skillSprite;
        public float manaCost;
        public float activeTime;
        public float skillEffectAmount; //Each skill will be aware of which attribute that they effect. 
        public float coolDown;

        public abstract void Cast();
        public abstract IEnumerator RevertSkillEffect(float time);
    }

}
