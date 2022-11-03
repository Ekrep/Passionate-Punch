using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{



    public class Chest : MonoBehaviour
    {
        public int itemIndex;
        public int chestCount;
        [SerializeField] private int maxChestCount; //Can be changed after trial and fail.
        [SerializeField] private List<ItemSettings> allItems = new List<ItemSettings>();
        public List<ItemSettings> chestList;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FillChest()
        {
            chestCount = Random.Range(1, maxChestCount);
            for(int i = 0; i < chestCount; i++){
                itemIndex = Random.Range(0, allItems.Count);
                ItemSettings tempItem = allItems[itemIndex];
                chestList.Add(tempItem);
                
            }
        }
    }
}
