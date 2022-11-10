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
    private void OnEnable()
    {
        warningText.gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesignSelectionScreen(ItemSettings item)
    {
        this.gameObject.SetActive(true);
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
        if (player.characterClass.ToString()!=item.itemType.ToString())
            warningText.gameObject.SetActive(true); //equipe taşınacak.
        Debug.Log(player.characterClass.ToString());
        Debug.Log(item.itemType.ToString());
       
        
    }
}
