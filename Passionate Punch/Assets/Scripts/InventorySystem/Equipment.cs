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

        [SerializeField]
        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }
        [SerializeField] private Inventory inventory;
        public static ItemSettings[] equipmentList;
        public static event Action OnEquipmentHappened;

        void Start()
        {
            int slotCount = System.Enum.GetNames(typeof(ItemSettings.ItemCategory)).Length;
            equipmentList = new ItemSettings[slotCount];
        }

        void OnEnable()
        {
            InventorySlot.OnItemEquip += EquipItem;
            InventorySlot.OnItemUnequip += UnEquipItem;
            InventorySlot.OnItemDiscard += DiscardItem;
        }

        void OnDisable()
        {
            InventorySlot.OnItemEquip -= EquipItem;
            InventorySlot.OnItemUnequip -= UnEquipItem;
            InventorySlot.OnItemDiscard -= DiscardItem;
        }

        public void DiscardItem(ItemSettings item, int index)
        {
            if (item != null && !item.isApplied)
            {
                _Character.ownedItemList.Remove(item);
                Inventory.inventoryList.Remove(item);
                OnEquipmentHappened?.Invoke();
            }

        }

        public void EquipItem(ItemSettings item, int index)
        {
            ItemSettings oldItem = null;

            if (equipmentList[index] != null)
            {
                oldItem = equipmentList[index];
                Inventory.inventoryList.Add(oldItem);
                _Character.equippedItemList.Remove(oldItem);
                oldItem.RevertItemEffect(_Character, oldItem.effectAmount);
            }
            equipmentList[index] = item;
            _Character.equippedItemList.Add(item);
            Inventory.inventoryList.Remove(item);
            item.ApplyItemEffect(_Character, item.effectAmount);
            OnEquipmentHappened?.Invoke();
        }

        public void UnEquipItem(ItemSettings item, int index)
        {
            item.RevertItemEffect(_Character, item.effectAmount);
            _Character.equippedItemList.Remove(item);
            Inventory.inventoryList.Add(item);
            equipmentList[index] = null;
            OnEquipmentHappened?.Invoke();
        }

        public bool CheckItemFit(ItemSettings item)
        {
            if (_Character.characterClass.Equals(item.itemType) || item.itemType == ClassType.ClassTypeEnum.All)
                return true;
            return false;
        }

        public void ApplyItemEffects(ItemSettings[] itemList)
        {
            foreach (ItemSettings item in itemList)
            {
                if (item != null)
                {
                    if (!item.isApplied)
                    {
                        item.ApplyItemEffect(_Character, item.effectAmount);
                    }
                }
            }
        }
    }
}

