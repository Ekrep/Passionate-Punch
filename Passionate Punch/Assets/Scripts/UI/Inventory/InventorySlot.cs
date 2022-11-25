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
        public static event Action<ItemSettings, int> OnItemDiscard;

        public void DisplayItem(ItemSettings newItem)
        {
            if (newItem != null)
            {
                item = newItem;
                slotIcon.sprite = newItem.itemImage;
                slotIcon.enabled = true;
            }
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
                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(() => OnItemEquip(item, (int)item.itemCategory));
                discardButton.onClick.RemoveAllListeners();
                discardButton.onClick.AddListener(() => OnItemDiscard(item, (int)item.itemCategory));
            }
        }

        public void OnEquippedItemChoose()
        {
            if (item != null)
            {
                index = ((int)item.itemCategory);
                equippedPanel.SetActive(true);
                unequipButton.onClick.RemoveAllListeners();
                unequipButton.onClick.AddListener(() => OnItemUnequip(item, (int)item.itemCategory));
            }
        }
    }
}