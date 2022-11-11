using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SkillSettings : ScriptableObject
    {
        private void OnEnable()
        {
            GameManager.OnSendCharacter += GameManager_OnSendCharacter;
            GameManager.OnResetSkills += GameManager_OnResetSkills;
        }

        private void GameManager_OnResetSkills()
        {
            canCast = true;
        }

        private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
        {
            Character = obj;
        }

        private void OnDisable()
        {
            GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
            GameManager.OnResetSkills -= GameManager_OnResetSkills;

        }
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
        [HideInInspector]
        public CharacterBaseStateMachine Character;

        public bool canCast;




        public abstract void Cast();
        public abstract IEnumerator RevertSkillEffect(float time);

        public abstract IEnumerator ExitCastState(float time);

        public abstract IEnumerator Cooldown(float time);



    }

}
