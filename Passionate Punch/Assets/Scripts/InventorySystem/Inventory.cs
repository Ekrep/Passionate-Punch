using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Items;


namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public static int inventorySize = 16; //can be changed later.
        public static IDictionary<Item, String> inventoryDict = new Dictionary<Item, String>(inventorySize);

        public void AddInventory(Item item)
        {
            if (CheckInventorySize())
            {
                inventoryDict.Add(item, item.GetComponent<ItemSettings>().itemTitle);
                item.PickedUp();

            }
        }

        public bool CheckInventorySize()
        {
            return inventoryDict.Count < inventorySize;
        }
    }
}


