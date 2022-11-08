using UnityEngine;
using UnityEngine.UI;
using Items;
using InventorySystem;

namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] public Image slotIcon;
        public ItemSettings item;

        public void DisplayItem(ItemSettings newItem)
        {
            item = newItem;
            slotIcon.sprite = item.itemImage;
            slotIcon.enabled = true;
        }

        public void DiscardItem()
        {

        }
    }
}
