using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using CharacterSystem;
namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/ClothItem")]

    public class ClothItem : ItemSettings
    {
        //Cloth items decrease player's health and mana recovery amount, increase move speed.
        public override void ApplyItemEffect(CharacterSettings player, float amount)
        {
            player.healthRecoveryAmount += amount;
            player.manaRecoveryAmount += amount;
            player.moveSpeed += amount;
        }

        public override void RevertItemEffect(CharacterSettings player, float amount)
        {
            player.healthRecoveryAmount -= amount;
            player.manaRecoveryAmount -= amount;
            player.moveSpeed -= amount;
        }

        public override void ConfigureDescription()
        {
            itemDescription = "";
        }
    }
}
