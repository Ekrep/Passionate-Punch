using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace Items
{
    public abstract class ItemSettings : ScriptableObject
    {
        //This is an abstract scriptable class that holds pure data about items. 
        public string itemTitle;
        public Sprite itemImage;
        public string itemDescription;
        public float effectAmount;
        public ItemClassType itemType;
        public enum ItemClassType
        {
            Assasin,
            Monk,
            Ranger,
            All
        }
        public abstract void ApplyItemEffect(float amount);
    }
}
