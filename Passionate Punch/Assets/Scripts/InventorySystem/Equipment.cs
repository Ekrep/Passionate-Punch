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
            InventorySlot.OnItemUnequip += UnEquipItem;
        }

        void OnDisable()
        {
            InventorySlot.OnItemEquip -= EquipItem;
            InventorySlot.OnItemUnequip -= UnEquipItem;
        }

        public void EquipItem(ItemSettings item, int index)
        {
            ItemSettings oldItem = null;

            if (equipmentList[index] != null)
            {
                oldItem = equipmentList[index];
                Inventory.inventoryList.Add(oldItem);
                character.equippedItemList.Remove(oldItem);
            }
            equipmentList[index] = item;
            character.equippedItemList.Add(item);
            Inventory.inventoryList.Remove(item);
            item.ApplyItemEffect(character, item.effectAmount);
            OnEquipmentHappened?.Invoke();
        }

        public void UnEquipItem(ItemSettings item, int index)
        {
            item.RevertItemEffect(character, item.effectAmount);
            character.equippedItemList.Remove(item);
            Inventory.inventoryList.Add(item);
            equipmentList[index] = null;
            OnEquipmentHappened?.Invoke();
        }   

    public bool CheckItemFit(ItemSettings item)
    {
        if (character.characterClass.Equals(item.itemType) || item.itemType == ClassType.ClassTypeEnum.All)
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
                    item.ApplyItemEffect(character, item.effectAmount);
                }
            }
        }
    }
}
}

