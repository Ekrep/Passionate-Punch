using UnityEngine;
using Interfaces;

namespace CharacterSystem
{
    public abstract class CharacterSettings : ScriptableObject, IHealth
    {
        public string characterName;
        public Sprite characterImage;
        public float health;
        public float attackDamage;
        public float attackSpeed;
        public float mana;
        public float moveSpeed;
        public float defence;
        public float range;
        public float recoveryTime;
        public float experience;

        public CharacterClassType characterClass;

        public enum CharacterClassType
        {
            Assasin,
            Monk,
            Ranger
        }

        public abstract void DecreaseHealth(float amount);
        public abstract void KillSelf();
    }

}

