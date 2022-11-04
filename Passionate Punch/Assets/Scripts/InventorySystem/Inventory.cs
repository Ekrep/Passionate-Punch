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
        
        public List<ItemSettings> inventoryList = new List<ItemSettings>(inventorySize); 
        public bool AddItem(ItemSettings item)
        {
            if (CheckInventorySize())
            {
                inventoryList.Add(item);
                //inventoryDict.Add(item, item.GetComponent<ItemSettings>());
                return true;
            }
            return false;
        }

        public void RemoveItem(ItemSettings item)
        {
            if(inventoryList.Contains(item))
            {
                inventoryList.Remove(item);
            }
        }

        public bool CheckInventorySize()
        {
            return inventoryList.Count < inventorySize;
        }
    }
}


