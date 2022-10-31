using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace Items
{
    [CreateAssetMenu(menuName = "Scriptables/Items/BasicSword")]
    public class BasicSword : ItemSettings
    {
        //This increases AD 5 unit.
        public BasicSword item;

        void Start()
        {
            item = GameObject.FindObjectOfType<BasicSword>();
        }

        public override void ApplyItemEffect(float amount)
        {
            throw new System.NotImplementedException();
        }

        public override void PickedUp()
        {
            Inventory.inventory.Add(this, this.itemTitle);

            Destroy(this.item);
        }
    }

}

