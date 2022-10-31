using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

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
            Assasin,
            Monk,
            Ranger,
            All
        }

        void OnEnable()
        {
            Inventory.onItemPickedUp += PickedUp;
        }

        void OnDisable()
        {
            Inventory.onItemPickedUp -= PickedUp;
        }
        public abstract void ApplyItemEffect(float amount);

        public abstract void PickedUp();
    }


}
