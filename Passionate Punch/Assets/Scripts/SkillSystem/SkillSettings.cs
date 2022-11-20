using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName ="Skill/SkillData")]
    public class SkillSettings : ScriptableObject
    {
        public GameObject skillPrefab;
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
        public float castTime;
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

        public MonoBehaviourSkill holder;
        
        public void Cast()
        {
            if (skillPrefab!=null&&canCast&&holder==null)
            {
                
                GameObject gO;
                gO = Instantiate(skillPrefab);
                gO.TryGetComponent<MonoBehaviourSkill>(out MonoBehaviourSkill skill);
                holder =skill;
                if (skill != null)
                {
                    skill.Cast();
                }
            }
            if (skillPrefab!=null&&canCast&&holder!=null)
            {
                holder.gameObject.SetActive(true);
                holder.Cast();
            }
                
            
           

        }



        //public abstract void Cast();
        //public abstract IEnumerator RevertSkillEffect(float time);

        //public abstract IEnumerator ExitCastState(float time);

        //public abstract IEnumerator Cooldown(float time);

        

    }

}
