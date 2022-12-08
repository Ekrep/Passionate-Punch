using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemCategories;

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
        [SerializeField] private List<ItemSettings> allItemSettings;
        public List<ItemSettings> chestList;
        public GameObject itemPrefab;

        // Start is called before the first frame update
        void Start()
        {
            FillChest();
        }


        public void FillChest()
        {
            chestCount = Random.Range(0, maxChestCount);
            for (int i = 0; i < chestCount; i++)
            {
                itemIndex = Random.Range(0, allItemSettings.Count);
                ItemSettings tempItem = allItemSettings[itemIndex];
                chestList.Add(tempItem);
                tempItem.countInInventory++;
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
            chestLid.GetComponent<Rigidbody>().isKinematic = false;
            chestLid.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.one * _explosionForce, chestLid.transform.up);
            StartCoroutine(ChestLidDissappear(_lidDissappearTime));
            miniMapIcon.DisableIcon();
            _isOpened = true;
            StartCoroutine(ExplodeChest());

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
            foreach (ItemSettings item in chestList)
            {
                itemPrefab.GetComponent<Item>().itemSettings = item;
                itemPrefab.GetComponent<Item>().itemSettings.ConfigureDescription();
                GameObject go = Instantiate(itemPrefab, new Vector3(this.gameObject.transform.position.x+this.gameObject.transform.forward.x,
                this.gameObject.transform.position.y+2f+ this.gameObject.transform.forward.y, this.gameObject.transform.position.z+ this.gameObject.transform.forward.z), Quaternion.Euler(0, 0, 90));
                
                if (item.countInInventory > 0)
                {
                    var instance = ScriptableObject.CreateInstance<ItemSettings>();
                    var cloneItemSettings = Instantiate(item);
                    instance = cloneItemSettings;
                    var settings = go.GetComponent<Item>().itemSettings;
                    go.GetComponent<Item>().itemSettings = (AttackItem) instance;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
