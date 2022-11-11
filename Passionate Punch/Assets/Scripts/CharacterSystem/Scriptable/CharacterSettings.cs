using UnityEngine;
using Interfaces;
using System.Collections.Generic;
using SkillSystem;

namespace CharacterSystem
{
    [CreateAssetMenu(menuName = "Scriptables/Classes/Character")]

    public class CharacterSettings : ScriptableObject
    {
        public string characterName;
        public Sprite characterImage;
        public Sprite inventorySprite;
        public float attackDamage;
        public float attackSpeed;
        public float maxHealth;
        public float mana;
        public float maxMana;
        public float moveSpeed;
        public float defence;
        public float range;
        public float AEORange; 
        public float healthRecoveryTime;
        public float healthRecoveryPeriod;
        public float healthRecoveryAmount;
        public float manaRecoveryTime;
        public float manaRecoveryPeriod;
        public float manaRecoveryAmount;
        public float experience;
        public int money;
        public List<SkillSettings> skillList;
        public ClassType.ClassTypeEnum characterClass;
    }
}

