using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill/SkillData")]
    public class SkillSettings : ScriptableObject
    {
        public GameObject skillPrefab;

        public Decal skillDecal;

       [HideInInspector]
        public Decal skillDecalFlag;

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
        public int skillIndex;
        public string description;
        public Sprite skillSprite;
        public float skillPureDamage;
        public float percenteOfCharacterAttackDamage;
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


        public float skillDamage
        {
            get
            {
                return skillPureDamage + (percenteOfCharacterAttackDamage * Character.characterStats.attackDamage / 100f);

            }
        }

        public bool canCast;

        private MonoBehaviourSkill _skillReference;

        public void Cast()
        {
            if (skillPrefab != null && canCast && _skillReference == null)
            {

                GameObject gO;
                gO = Instantiate(skillPrefab);
                gO.TryGetComponent(out MonoBehaviourSkill skill);
                _skillReference = skill;
                if (_skillReference != null)
                {
                    _skillReference.Cast();
                    if (skillDecal!=null)
                    {
                        skillDecalFlag.gameObject.SetActive(false);
                    }
                   

                }
            }
            if (skillPrefab != null && canCast && _skillReference != null)
            {
                _skillReference.gameObject.SetActive(true);
                _skillReference.Cast();
                if (skillDecal!=null)
                {
                    skillDecalFlag.gameObject.SetActive(false);
                }
                
            }




        }

        public void CreateDecal(Vector3 joystickPos)
        {
            if (canCast&&skillDecal!=null&&Character.currentState!=Character.characterSkillCastState)
            {
                //Instantate and reference
                if (skillDecalFlag != null)
                {
                    Debug.Log("decall");
                    skillDecalFlag.gameObject.SetActive(true);
                    skillDecalFlag.gameObject.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y - 0.02f, Character.transform.position.z);
                    //_skillDecalFlag.circleProjector.transform.position = Character.transform.position;
                    skillDecalFlag.SetDecalPosAndRot(Character.transform.position, joystickPos);
                }
                if (skillDecal != null && skillDecalFlag == null)
                {
                    GameObject decal;
                    decal = Instantiate(skillDecal.gameObject);
                    
                    Debug.Log("decall");
                    skillDecalFlag = decal.GetComponent<Decal>();
                    skillDecalFlag.gameObject.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y - 0.02f, Character.transform.position.z);
                    //_skillDecalFlag.circleProjector.transform.position = Character.transform.position;
                    skillDecalFlag.SetDecalPosAndRot(Character.transform.position, joystickPos);



                }
            }
           



        }



        //public abstract void Cast();
        //public abstract IEnumerator RevertSkillEffect(float time);

        //public abstract IEnumerator ExitCastState(float time);

        //public abstract IEnumerator Cooldown(float time);



    }

}
