using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using CharacterSystem;

namespace Items
{
    [CreateAssetMenu(menuName = "Scriptables/Items/BasicSword")]
    public class BasicSword : ItemSettings
    {
        //This increases AD 5 unit.
        [SerializeField] private CharacterSettings player;

        public override void ApplyItemEffect(float amount)
        {
            //Will be called when player attach the item to her inventory.
            player.attackDamage += amount;
        }
        
        public override void RevertItemEffect(float amount)
        {
            //A method for undo effect when an item has been unequipped
            player.attackDamage -= amount;
        }

    }

}

