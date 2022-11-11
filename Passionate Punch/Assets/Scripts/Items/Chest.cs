using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Chest : MonoBehaviour
    {
        [Header("MiniMapIcon")]
        public MiniMapIcon miniMapIcon;


        [Header("Lid")]
        public GameObject chestLid;
        [SerializeField]
        private float _explosionForce;
        [SerializeField]
        private float _lidDissappearTime;

        private bool _isOpened;


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
            if (!_isOpened)
            {
                OpenChestX();
            }
            
        }


       

        public void OpenChestX()
        {
            Debug.Log(chestLid.transform.rotation);
            chestLid.GetComponent<Rigidbody>().isKinematic = false;
            chestLid.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.one * _explosionForce, chestLid.transform.up);
            StartCoroutine(ChestLidDissappear(_lidDissappearTime));
            miniMapIcon.DisableIcon();
            _isOpened = true;


           
            

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, 2f);
        }

        IEnumerator ChestLidDissappear(float dissappearTime)
        {
            yield return new WaitForSeconds(dissappearTime);
            chestLid.SetActive(false);
            this.enabled = false;


        }





    }
}
