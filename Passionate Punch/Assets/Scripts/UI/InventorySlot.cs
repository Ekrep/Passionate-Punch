using UnityEngine.UI;
using UnityEngine;
using Items;

namespace UI
{

    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image slotIcon;
        [SerializeField] private ItemSelectionUI selectionUI;
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

        public void onItemChoose()
        {
            if(item != null){
                selectionUI.DesignSelectionScreen(item);
            }
        }
    }
}