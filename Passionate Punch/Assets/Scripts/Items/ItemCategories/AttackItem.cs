using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/AttackItem")]


    public class AttackItem : ItemSettings
    {
        //Attack items increase player's attack damage and speed.
        public override void ApplyItemEffect(float amount)
        {
            player.attackDamage += amount;
            player.attackSpeed += amount / 10;
        }

        public override void RevertItemEffect(float amount)
        {
            player.attackDamage -= amount;
            player.attackSpeed -= amount / 10;
        }

    }
}
