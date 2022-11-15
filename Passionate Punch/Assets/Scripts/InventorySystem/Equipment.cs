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
            //InventorySlot.OnItemUnequip += UnEquipItem;
        }

        void OnDisable()
        {
            InventorySlot.OnItemEquip -= EquipItem;
            //InventorySlot.OnItemUnequip -= UnEquipItem;
        }

        public void EquipItem(ItemSettings item)
        {
            int slotIndex = (int)item.itemCategory;
            Debug.Log("slot index is :" + slotIndex);
            ItemSettings oldItem = null;

            if (equipmentList[slotIndex] != null)
            {
                oldItem = equipmentList[slotIndex];
                Inventory.inventoryList.Add(oldItem);
            }
            equipmentList[slotIndex] = item;
            Inventory.inventoryList.Remove(item);
            OnEquipmentHappened?.Invoke();
            ApplyItemEffects(equipmentList);
        }

        /*public void UnEquipItem(ItemSettings item)
        {
            if (equipmentList.Contains(item))
            {
                item.RevertItemEffect(character, item.effectAmount);
                inventory.AddItem(item);
                equipmentList.Remove(item);
                OnEquipmentHappened?.Invoke();
                ApplyItemEffects(equipmentList);
                item.isApplied = false;
            }
        }*/

        public bool CheckItemFit(ItemSettings item)
        {
            Debug.Log(character.characterClass.Equals(item.itemType));
            if (character.characterClass.Equals(item.itemType) || item.itemType == ClassType.ClassTypeEnum.All)
                return true;
            return false;
        }

        public void ApplyItemEffects(ItemSettings[] itemList)
        {
            foreach (ItemSettings item in itemList)
            {
                if (!item.isApplied)
                {
                    item.ApplyItemEffect(character, item.effectAmount);
                    item.isApplied = true;
                }
            }
        }
    }

}

