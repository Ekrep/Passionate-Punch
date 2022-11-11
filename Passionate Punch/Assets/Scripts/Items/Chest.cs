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
        [SerializeField] private List<Item> allItems;
        public List<Item> chestList;

        // Start is called before the first frame update
        void Start()
        {
           // FillChest();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FillChest()
        {
            chestCount = Random.Range(1, maxChestCount);
            for (int i = 0; i < chestCount; i++)
            {
                itemIndex = Random.Range(0, allItems.Count);
                Item tempItem = allItems[itemIndex];
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
            //miniMapIcon.DisableIcon();
            _isOpened = true;
           // StartCoroutine(ExplodeChest());

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

        IEnumerator ExplodeChest()
        {
            yield return new WaitForSeconds(1f);
            foreach (Item item in chestList)
            {
                Instantiate(item.gameObject, new Vector3(this.gameObject.transform.position.x,
                this.gameObject.transform.position.y, this.gameObject.transform.position.z + 2.5f), Quaternion.identity);

                //item.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.one * _explosionForce, chestLid.transform.up);
            }

        }
    }
}
