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

        public List<Item> equipmentList = new List<Item>(equipmentSize);
        public void EquipItem(Item item)
        {
            if (equipmentList.Count < equipmentSize && CheckItemFit(item))
            {
                equipmentList.Add(item);
                inventory.RemoveItem(item);
                ApplyItemEffects(equipmentList);
            }
        }

        public void UnclotheItem(Item item)
        {
            ItemSettings itemTemp = item.GetComponent<ItemSettings>();
            if (equipmentList.Contains(item))
            {
                itemTemp.isApplied = false;
                itemTemp.RevertItemEffect(itemTemp.effectAmount);
                inventory.AddItem(item);
                equipmentList.Remove(item);
                ApplyItemEffects(equipmentList);
            }
        }

        public bool CheckItemFit(Item item)
        {
            return character.characterClass.Equals(item.GetComponent<ItemSettings>().itemType);
        }

        public void ApplyItemEffects(List<Item> itemList)
        {
            foreach (Item item in itemList)
            {
                ItemSettings temp = item.GetComponent<ItemSettings>();
                if (!temp.isApplied)
                {
                    temp.ApplyItemEffect(temp.effectAmount);
                    temp.isApplied = true;
                }
            }
        }
    }

}

