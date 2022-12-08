using UnityEngine;
using CharacterSystem;

namespace Items
{
    public class ItemSettings : ScriptableObject
    {
        //This is an abstract scriptable class that holds pure data about items. 
        public Mesh itemMesh;
        public Material itemMaterial;
        public string itemTitle;
        public Sprite itemImage;
        public string itemDescription;
        public float effectAmount;
        public float worth;
        public float radius;
        public bool isStackable;
        public bool isApplied;
        public int countInInventory;
        public ItemCategory itemCategory;
        public enum ItemCategory 
        {
            Head,
            Chest,
            Weapon,
            Shield,
            Feet,
            Charm,
        }
        public ClassType.ClassTypeEnum itemType;
        public virtual void ApplyItemEffect(CharacterSettings player, float amount){}
        public virtual void RevertItemEffect(CharacterSettings player, float amount){}
        public virtual void ConfigureDescription(){}
    }
}
