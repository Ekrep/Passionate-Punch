using UnityEngine;
using InventorySystem;

namespace UI
{
    public class EquipmentUI : MonoBehaviour
    {
        InventorySlot[] equipmentSlots;
        void Start()
        {
            equipmentSlots = this.GetComponentsInChildren<InventorySlot>();

        }

        void OnEnable()
        {
            Equipment.OnEquipmentHappened += UpdateUI;
            InventorySlot.OnItemDiscard += UpdateUI;
        }

        void OnDisable()
        {
            Equipment.OnEquipmentHappened -= UpdateUI;
            InventorySlot.OnItemDiscard -= UpdateUI;
        }

        void UpdateUI()
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (Equipment.equipmentList[i] != null)
                {
                    equipmentSlots[i].DisplayItem(Equipment.equipmentList[i]);
                }
                else
                {
                    equipmentSlots[i].ClearSlot();
                }
            }
        }
    }
}