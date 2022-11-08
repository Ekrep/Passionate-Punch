using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using Items;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        InventorySlot[] slots;

        void Start()
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        void Update()
        {

        }

        void OnEnable()
        {
            Inventory.onItemPickedUp += UpdateUI;
        }

        void OnDisable()
        {
            Inventory.onItemPickedUp -= UpdateUI;
        }

        void UpdateUI()
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(i < Inventory.inventoryList.Count)
                {
                    slots[i].DisplayItem(Inventory.inventoryList[i]);
                }
            }
        }
    }
}
