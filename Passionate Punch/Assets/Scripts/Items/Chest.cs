using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Chest : MonoBehaviour
    {
        [Header("Lid")]
        public GameObject chestLid;
        public float lidOpenSpeed;
        [SerializeField] private float _lidOpenAngle;


        [Header("Parameters")]
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


        public void Open()
        {
            StartCoroutine(ChestOpener());
        }


        IEnumerator ChestOpener()
        {
            yield return new WaitForEndOfFrame();
            OpenChestX();


        }

        public void OpenChestX()
        {
            Debug.Log(chestLid.transform.rotation);
            
            chestLid.transform.rotation = Quaternion.RotateTowards(chestLid.transform.rotation, Quaternion.Euler(_lidOpenAngle, 0, 0), lidOpenSpeed * Time.deltaTime);
            if (chestLid.gameObject.transform.rotation.x>-0.7f)
            {
                StartCoroutine(ChestOpener());
            }
            else 
            {
                StopCoroutine(ChestOpener());
                this.enabled = false;
            }
            

        }

       
       


    }
}
