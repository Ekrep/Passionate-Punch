using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using Items;

namespace InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        //This is the class that handles equipment of items. 
        [SerializeField] private Inventory inventory;
        [SerializeField] private CharacterSettings character;
        public static int equipmentSize = 5;

        public List<ItemSettings> equipmentList = new List<ItemSettings>(equipmentSize);
        public void EquipItem(ItemSettings item)
        {
            if (equipmentList.Count < equipmentSize && CheckItemFit(item))
            {
                equipmentList.Add(item);
                inventory.RemoveItem(item);
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

