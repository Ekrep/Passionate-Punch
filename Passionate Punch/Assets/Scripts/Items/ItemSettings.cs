using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public abstract class ItemSettings : ScriptableObject
    {
        public string itemTitle;
        public Sprite itemImage;
        public string itemDescription;
        public float effectAmount;
        public float inventorySize;
        public ItemClassType itemType;
        public enum ItemClassType
        {
            Monk,
            Assasin,
            Ranger,
            All
        }
        public abstract void ApplyItemEffect(float amount);
    }


}
