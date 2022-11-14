using UnityEngine;
using CharacterSystem;

namespace Items
{
    public abstract class ItemSettings : ScriptableObject
    {
        //This is an abstract scriptable class that holds pure data about items. 
        public CharacterSettings player;
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
        public ClassType.ClassTypeEnum itemType;
        public abstract void ApplyItemEffect(float amount);
        public abstract void RevertItemEffect(float amount);
    }
}
