using UnityEngine;
using InventorySystem;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        InventorySlot[] slots;
        [SerializeField] public Image playerImageField;
        [SerializeField] public Sprite playerImage;

        void Start()
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            playerImageField.sprite = playerImage;
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
                    Debug.Log(Inventory.inventoryList[i]);
                    slots[i].DisplayItem(Inventory.inventoryList[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        public void ClosePanel()
        {
            this.gameObject.SetActive(false);
        }
    }
}
