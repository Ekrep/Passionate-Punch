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
        public static IDictionary<ItemSettings, String> inventoryDict = new Dictionary<ItemSettings, String>(inventorySize);
        public static event Action onItemPickedUp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckInventory()
        {
            if(inventoryDict.Count < inventorySize)
            {
                onItemPickedUp?.Invoke();
            }
        }
    }

}

