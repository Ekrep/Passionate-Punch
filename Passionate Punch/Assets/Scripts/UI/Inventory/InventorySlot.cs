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
        [SerializeField]
        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }
        [SerializeField] private Image slotIcon;
        [SerializeField] private ItemSelectionUI selectionUI;
        [SerializeField] private GameObject equippedPanel;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button unequipButton;
        [SerializeField] private Button discardButton;
        public Sprite defaultImage;
        public ItemSettings item;
        public int index;

        public static event Action<ItemSettings, int> OnItemEquip;
        public static event Action<ItemSettings, int> OnItemUnequip;

        public void DisplayItem(ItemSettings newItem)
        {
            if (newItem != null)
            {
                item = newItem;
                slotIcon.sprite = newItem.itemImage;
                slotIcon.enabled = true;
                newItem = null;
            }
        }

        public void DiscardItem()
        {
            if (item != null)
            {
                if (item.isApplied)
                {
                    _Character.equippedItemList.Remove(item);   
                    item.RevertItemEffect(_Character, item.effectAmount);
                }
                Equipment.equipmentList[index] = null;
                Inventory.inventoryList.Remove(item);
                _Character.ownedItemList.Remove(item);
            
            }
            ClearSlot();
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
                index = ((int)item.itemCategory);
                selectionUI.DesignSelectionScreen(item);
                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(() => OnItemEquip(item, index));
                discardButton.onClick.RemoveAllListeners();
                discardButton.onClick.AddListener(() => DiscardItem());
            }
        }

        public void OnEquippedItemChoose()
        {
            if (item != null)
            {
                equippedPanel.SetActive(true);

                unequipButton.onClick.RemoveAllListeners();
                unequipButton.onClick.AddListener(() => OnUnequipButtonPressed());
                discardButton.onClick.RemoveAllListeners();
                discardButton.onClick.AddListener(() => DiscardItem());

            }
        }

        public void OnEquipButtonPressed()
        {
            OnItemEquip?.Invoke(item, index);
        }

        public void OnUnequipButtonPressed()
        {
            index = ((int)item.itemCategory);
            if (Equipment.equipmentList[index] != null)
            {
                OnItemUnequip?.Invoke(item, index);
                equippedPanel.SetActive(false);
            }
        }
    }
}