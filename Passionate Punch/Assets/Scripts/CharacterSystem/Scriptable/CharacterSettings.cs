using UnityEngine;
using Interfaces;
using System.Collections.Generic;
using SkillSystem;
using Items;

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
        public float attackRayThickness;
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
        public float experienceThreshold;
        public float level;
        public int money;
        public List<SkillSettings> skillList;
        public List<ItemSettings> ownedItemList;
        public List<ItemSettings> equippedItemList;
        public List<float> characterStats;
        public ClassType.ClassTypeEnum characterClass;

        public void LevelUp()
        {
            List<float> stats = new List<float>{attackDamage, attackSpeed, maxHealth, maxMana,
            defence, healthRecoveryAmount, manaRecoveryAmount};

            for (int i = 0; i < stats.Count; i++)
            {
                stats[i] += 2;
            }

            healthRecoveryTime -= 0.15f;
            manaRecoveryTime -= 0.05f;
            level++;
            experienceThreshold += 30;
        }
    }
}

