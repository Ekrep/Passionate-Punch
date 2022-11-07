using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using Items;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/DefenceItem")]

    public class DefenceItem : ItemSettings
    {
        //Defence items increase player's health and defence.
        public override void ApplyItemEffect(float amount)
        {
            player.maxHealth += amount;
            player.defence += amount;
        }

        public override void RevertItemEffect(float amount)
        {
            player.maxHealth -= amount;
            player.defence -= amount;
        }
    }
}
