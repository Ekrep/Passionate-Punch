using System.Collections.Generic;
using UnityEngine;
using System;
using CharacterSystem;
using Items;
using UI;

namespace InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        //This is the class that handles equipment of items. 
        [SerializeField] private Inventory inventory;
        [SerializeField] private CharacterSettings character;
        public static int equipmentSize = 5;
        public static List<ItemSettings> equipmentList;
        public static event Action OnEquipmentHappened;

        void Start()
        {
            equipmentList = new List<ItemSettings>(equipmentSize);
        }

        void OnEnable()
        {
            InventorySlot.OnItemEquip += EquipItem;
        }

        void OnDisable()
        {
            InventorySlot.OnItemEquip -= EquipItem;
        }

        public void EquipItem(ItemSettings item)
        {
            if (equipmentList.Count < equipmentSize)
            {
                equipmentList.Add(item);
                Inventory.inventoryList.Remove(item);
                OnEquipmentHappened?.Invoke();
                ApplyItemEffects(equipmentList);
            }
        }

        public void UnEquipItem(ItemSettings item)
        {
            if (equipmentList.Contains(item))
            {
                item.isApplied = false;
                item.RevertItemEffect(item.effectAmount);
                inventory.AddItem(item);
                equipmentList.Remove(item);
                ApplyItemEffects(equipmentList);
            }
        }

        public bool CheckItemFit(ItemSettings item)
        {
            return character.characterClass.Equals(item.itemType);
        }

        public void ApplyItemEffects(List<ItemSettings> itemList)
        {
            foreach (ItemSettings item in itemList)
            {
                if (!item.isApplied)
                {
                    item.ApplyItemEffect(item.effectAmount);
                    item.isApplied = true;
                }
            }
        }
    }

}

