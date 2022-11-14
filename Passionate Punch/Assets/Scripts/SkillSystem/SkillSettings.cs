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
        public enum SkillType
        {
            Passive,
            Active
        }

        public SkillType skillType;
        public HitType hitType;
        public string animationName;
        public string skillName;
        public string description;
        public Sprite skillSprite;
        public float manaCost;
        public float activeTime;
        public float skillEffectAmount; //Each skill will be aware of which attribute that they effect. 
        public float coolDown;
        public float stackCastCoolDown;

        public int baseStackCount;
        [HideInInspector]
        public int stackCount;
        [HideInInspector]
        public CharacterBaseStateMachine Character
        {
            get
            {
                return GameManager.Instance.character;
            }
        }

        public bool canCast;

        




        public abstract void Cast();
        public abstract IEnumerator RevertSkillEffect(float time);

        public abstract IEnumerator ExitCastState(float time);

        public abstract IEnumerator Cooldown(float time);

        

    }

}
