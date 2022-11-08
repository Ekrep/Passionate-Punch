using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Items;
using CharacterSystem;

public class ItemSelectionUI : MonoBehaviour
{
    [SerializeField] CharacterSettings player;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI warningText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesignSelectionScreen(ItemSettings item)
    {
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
        if(!player.characterClass.ToString().Equals(item.itemType.ToString()))
            warningText.enabled = true;
        this.gameObject.SetActive(true);
    }
}
