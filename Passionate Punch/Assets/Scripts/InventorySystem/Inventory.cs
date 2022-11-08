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
        public List<ItemSettings> inventoryList = new List<ItemSettings>(inventorySize); 
        public static event Action onItemPickedUp;
        public bool AddItem(ItemSettings item)
        {
            if (CheckInventorySize())
            {
                inventoryList.Add(item);
                onItemPickedUp?.Invoke();
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


