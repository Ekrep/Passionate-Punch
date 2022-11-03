using UnityEngine;
using Interfaces;

namespace CharacterSystem
{
    [CreateAssetMenu(menuName = "Scriptables/Classes/Character")]

    public abstract class CharacterSettings : ScriptableObject
    {
        public string characterName;
        public Sprite characterImage;
        public float attackDamage;
        public float attackSpeed;
        public float mana;
        public float moveSpeed;
        public float defence;
        public float range;
        public float recoveryTime;
        public float recoveryPeriod;
        public float recoveryAmount;
        public float experience;

        public CharacterClassType characterClass;
        public enum CharacterClassType
        {
            Assasin,
            Monk,
            Ranger
        }
    }
}

