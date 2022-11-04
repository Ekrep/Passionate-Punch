using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/CharmItem")]

    public class CharmItem : ItemSettings
    {
        //Charm items increase player's attack damage and max mana.
        public override void ApplyItemEffect(float amount)
        {
            player.attackDamage += amount;
            player.maxMana += amount;
        }

        public override void RevertItemEffect(float amount)
        {
            player.attackDamage -= amount;
            player.maxMana -= amount;
        }
    }
}
