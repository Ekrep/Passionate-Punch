using UnityEngine;
using InventorySystem;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        InventorySlot[] slots;
        public ItemSelectionUI selectionUI;
        public GameObject unequipPanel;

        void Start()
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        void OnEnable()
        {
            Inventory.OnItemPickedUp += UpdateUI;
            Equipment.OnEquipmentHappened += UpdateUI;
        }

        void OnDisable()
        {
            Inventory.OnItemPickedUp -= UpdateUI;
            Equipment.OnEquipmentHappened -= UpdateUI;
        }

        void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < Inventory.inventoryList.Count)
                {
                    slots[i].DisplayItem(Inventory.inventoryList[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
            unequipPanel.SetActive(false);
        }

        public void CloseAllPanels()
        {
            CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            selectionUI.gameObject.SetActive(false);
            unequipPanel.SetActive(false);
        }
        public void ClosePanel()
        {
            selectionUI.gameObject.SetActive(false);
        }
    }
}
