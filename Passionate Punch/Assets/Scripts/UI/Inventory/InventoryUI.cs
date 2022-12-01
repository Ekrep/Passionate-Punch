using UnityEngine;
using InventorySystem;
using UnityEngine.UI;
using CharacterSystem;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {

        [SerializeField]
        private CharacterSettings _Character;
        public Transform itemsParent;
        public Image playerImage;
        InventorySlot[] slots;
        public ItemSelectionUI selectionUI;
        public GameObject unequipPanel;

        void Start()
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            
        }

        void GameManager_OnSendCharacter(CharacterBaseStateMachine obj){
            _Character = obj.characterStats;
            playerImage.sprite = _Character.inventorySprite;
            
        }
        void OnEnable()
        {
            GameManager.OnSendCharacter += GameManager_OnSendCharacter;
            Inventory.OnItemPickedUp += UpdateUI;
            Equipment.OnEquipmentHappened += UpdateUI;
        }

        void OnDisable()
        {
            GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
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
