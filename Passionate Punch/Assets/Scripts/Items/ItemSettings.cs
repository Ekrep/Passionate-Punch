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

        public bool hasPickedUp;
        public ItemClassType itemType;
        public enum ItemClassType
        {
            Assasin,
            Monk,
            Ranger,
            All
        }
        public abstract void ApplyItemEffect(float amount);

        public abstract void PickedUp();
    }


}
