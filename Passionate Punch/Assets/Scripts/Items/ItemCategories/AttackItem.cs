using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using CharacterSystem;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/AttackItem")]

    public class AttackItem : ItemSettings
    {
        //Attack items increase player's attack damage and speed.
        public float speedEffectAmount;

        public override void ApplyItemEffect(CharacterSettings player, float amount)
        {
            player.attackDamage += amount;
            player.attackSpeed += speedEffectAmount;
            isApplied = true;
        }

        public override void RevertItemEffect(CharacterSettings player, float amount)
        {
            player.attackDamage -= amount;
            player.attackSpeed -= speedEffectAmount;
            isApplied = false;
            
        }

        public override void ConfigureDescription()
        {
            itemDescription = "+" + effectAmount + " AD, +" + speedEffectAmount + " Attack Speed";
        }
    }
}
