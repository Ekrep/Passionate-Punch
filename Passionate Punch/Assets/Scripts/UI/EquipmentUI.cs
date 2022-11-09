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
        }

        void OnDisable()
        {
            Equipment.OnEquipmentHappened -= UpdateUI;
        }

        void UpdateUI()
        {
            for(int i = 0; i < equipmentSlots.Length; i++)
            {
                if(i < Equipment.equipmentList.Count)
                {
                    equipmentSlots[i].DisplayItem(Equipment.equipmentList[i]);
                }
            }
        }
    }
}