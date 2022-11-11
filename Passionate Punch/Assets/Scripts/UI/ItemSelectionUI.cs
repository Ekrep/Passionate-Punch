using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Items;
using CharacterSystem;
using InventorySystem;

public class ItemSelectionUI : MonoBehaviour
{
    [SerializeField] CharacterSettings player;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject equipButton;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisableEquipButton()
    {
        warningText.gameObject.SetActive(true);
        equipButton.SetActive(false);
    }

    void EnableEquipButton()
    {
        warningText.gameObject.SetActive(false);
        equipButton.SetActive(true);
    }

    public void DesignSelectionScreen(ItemSettings item)
    {
        this.gameObject.SetActive(true);
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
        if (player.characterClass != item.itemType && item.itemType != ClassType.ClassTypeEnum.All)
        {
            DisableEquipButton();
        }
        else
        {
            EnableEquipButton();
        }

    }
}

