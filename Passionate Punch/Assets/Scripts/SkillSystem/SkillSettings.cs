using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SkillSettings : ScriptableObject
    {
        public enum HitType
        {
            Low,
            Medium,
            Hard
        }
        /*public enum SkillType => Not necessary
        {
            Passive,
            Active
        }*/

        public HitType hitType;
        public string animationName;
        public string skillName;
        public string description;
        public Sprite skillSprite;
        public float manaCost;
        public float activeTime;
        public float skillEffectAmount; //Each skill will be aware of which attribute that they effect. 
        public float coolDown;

        public abstract void Cast();
        public abstract IEnumerator RevertSkillEffect(float time);
    }

}
