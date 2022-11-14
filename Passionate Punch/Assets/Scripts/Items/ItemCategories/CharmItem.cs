using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using CharacterSystem;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/CharmItem")]

    public class CharmItem : ItemSettings
    {
        //Charm items increase player's attack damage and max mana.
        public override void ApplyItemEffect(CharacterSettings player, float amount)
        {
            player.attackDamage += amount;
            player.maxMana += amount;
        }

        public override void RevertItemEffect(CharacterSettings player, float amount)
        {
            player.attackDamage -= amount;
            player.maxMana -= amount;
        }

        public override void ConfigureDescription()
        {
            itemDescription = "";
        }
    }
}
