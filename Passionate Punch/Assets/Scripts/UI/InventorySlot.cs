using UnityEngine.UI;
using UnityEngine;
using Items;
using System;

namespace UI
{

    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image slotIcon;
        [SerializeField] private ItemSelectionUI selectionUI;
        [SerializeField] private GameObject equippedPanel;
        public Sprite defaultImage;
        public ItemSettings item;
        public static event Action<ItemSettings> OnItemEquip;
        public static event Action<ItemSettings> OnItemUnequip;

        public void DisplayItem(ItemSettings newItem)
        {
            if (newItem != null)
            {
                item = newItem;
                slotIcon.sprite = item.itemImage;
                slotIcon.enabled = true;
            }

        }

        public void DiscardItem()
        {
            item.RevertItemEffect(item.effectAmount);
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
            equippedPanel.SetActive(true);
        }

        public void OnEquipButtonPressed()
        {
            OnItemEquip?.Invoke(item);
        }

        public void OnUnequipButtonPressed()
        {
            OnItemUnequip?.Invoke(item);
            equippedPanel.SetActive(false);
        }
    }
}