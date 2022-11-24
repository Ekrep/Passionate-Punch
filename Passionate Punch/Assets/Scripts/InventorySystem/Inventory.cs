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
        public static List<ItemSettings> inventoryList; 
        public static event Action OnItemPickedUp;
        private CharacterBaseStateMachine _Character{
            get {
                return GameManager.Instance.character;
            }
        }

        void Start()
        {
            inventoryList = new List<ItemSettings>(inventorySize);
        }

        public bool AddItem(ItemSettings item)
        {
            if (CheckInventorySize())
            {
                inventoryList.Add(item);
                Debug.Log("saaaaaaaaa" + inventoryList[inventoryList.Count - 1].effectAmount);
                _Character.characterStats.ownedItemList.Add(item);
                OnItemPickedUp?.Invoke();
                return true;
            }
            return false;
        }

        public bool CheckInventorySize()
        {
            return inventoryList.Count < inventorySize;
        }
    }
}


