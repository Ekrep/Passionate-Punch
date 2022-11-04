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
        public bool isStackable;
        public bool isApplied;
        public int countInInventory;
        public ItemClassType itemType;
        public enum ItemClassType
        {
            Assasin,
            Ranger,
            All
        }
        public abstract void ApplyItemEffect(float amount);
        public abstract void RevertItemEffect(float amount);
    }
}
