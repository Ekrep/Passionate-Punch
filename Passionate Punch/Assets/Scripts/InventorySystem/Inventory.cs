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
        //public static IDictionary<Item, ItemSettings> inventoryDict = new Dictionary<Item, ItemSettings>(inventorySize);
        public static List<Item> inventoryList = new List<Item>(inventorySize); 
        public void AddInventory(Item item)
        {
            if (CheckInventorySize())
            {
                inventoryList.Add(item);
                //inventoryDict.Add(item, item.GetComponent<ItemSettings>());
                item.PickedUp();
            }
        }

        public bool CheckInventorySize()
        {
            return inventoryList.Count < inventorySize;
        }
    }
}


