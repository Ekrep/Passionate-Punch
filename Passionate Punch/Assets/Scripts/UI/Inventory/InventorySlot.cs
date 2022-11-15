using UnityEngine.UI;
using UnityEngine;
using Items;
using System;
using CharacterSystem;
using InventorySystem;

namespace UI
{

    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private CharacterSettings character;
        [SerializeField] private Image slotIcon;
        [SerializeField] private ItemSelectionUI selectionUI;
        [SerializeField] private GameObject equippedPanel;
        public Sprite defaultImage;
        public ItemSettings item;
        public int index;
        public static event Action<ItemSettings> OnItemEquip;
        public static event Action<int> OnItemUnequip;

        public void DisplayItem(ItemSettings newItem)
        {
            if (newItem != null)
            {
                item = newItem;
                slotIcon.sprite = newItem.itemImage;
                slotIcon.enabled = true;
            }
        }

        public void DiscardItem()
        {
            if (item != null)
            {
                if (item.isApplied)
                {
                    item.RevertItemEffect(character, item.effectAmount);
                    item.isApplied = false;
                }
            }

            item = null;
            slotIcon.sprite = defaultImage;
            slotIcon.enabled = true;
            selectionUI.gameObject.SetActive(false);
            equippedPanel.SetActive(false);
        }

        public void ClearSlot()
        {
            item = null;
            slotIcon.sprite = defaultImage;
            slotIcon.enabled = true;
            selectionUI.gameObject.SetActive(false);
        }

        public void OnItemChoose()
        {
            if (item != null)
            {
                selectionUI.DesignSelectionScreen(item);
            }
        }

        public void OnEquippedItemChoose()
        {
            
            if (item != null)
                equippedPanel.SetActive(true);
                for(int i = 0; i <Equipment.equipmentList.Length; i++)
                {
                    if (Equipment.equipmentList[i] == item)
                    {
                        index = i;
                    }
                }
        }

        public void OnEquipButtonPressed()
        {
            OnItemEquip?.Invoke(item);
        }

        public void OnUnequipButtonPressed()
        {
            if (Equipment.equipmentList[index] != null)
            {
                Debug.Log("index" + index);
                OnItemUnequip?.Invoke(index);
                equippedPanel.SetActive(false);
            }
        }
    }
}