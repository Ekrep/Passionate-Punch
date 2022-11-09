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
        public Sprite defaultImage;
        public ItemSettings item;
        public static event Action<ItemSettings> OnItemEquip;

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
            item = null;
            slotIcon.sprite = defaultImage;
            slotIcon.enabled = true;
            selectionUI.gameObject.SetActive(false);
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

        public void OnEquipButtonPressed()
        {
            OnItemEquip?.Invoke(item);
        }
    }
}