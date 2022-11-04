using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace ItemCategories
{
    [CreateAssetMenu(menuName = "Scriptables/Items/ClothItem")]

    public class ClothItem : ItemSettings
    {
        //Cloth items decrease player's health and mana recovery amount, increases movespeed.
        public override void ApplyItemEffect(float amount)
        {
            player.healthRecoveryAmount += amount;
            player.manaRecoveryAmount += amount;
            player.moveSpeed += amount;
        }

        public override void RevertItemEffect(float amount)
        {
            player.healthRecoveryAmount -= amount;
            player.manaRecoveryAmount -= amount;
            player.moveSpeed -= amount;
        }
    }
}
