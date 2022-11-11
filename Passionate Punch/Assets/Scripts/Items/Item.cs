using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using TMPro;


namespace Items
{
    public class Item : MonoBehaviour
    {
        //The purpose of this class is to handle various functionalities that items will have apart form data class. 
        [SerializeField] public ItemSettings itemSettings;
        [SerializeField] public Transform player;
        [SerializeField] private Rigidbody rigidBody;
        public Rigidbody Rigidbody => rigidBody;
        [SerializeField] public GameObject itemText;
        
        void Start()
        {
            //GetComponent<SpriteRenderer>().sprite = itemSettings.itemImage;
            GetComponent<MeshFilter>().mesh = itemSettings.itemMesh;
        }

        void Update()
        {
            CheckDistance();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Inventory>()!=null)
            {
                if (collider.gameObject.GetComponent<Inventory>().AddItem(this.itemSettings))
                {
                    PickedUp();
                }    
            }
        }

        void CheckDistance()
        {
            if(player.position.x - this.transform.position.x < itemSettings.radius || 
            player.position.z - this.transform.position.z < itemSettings.radius)
            {
                itemText.GetComponent<TextMeshProUGUI>().text = itemSettings.itemTitle;
                itemText.SetActive(true);
            }
        }

        public void PickedUp()
        {
            Destroy(this.gameObject);
        }
    }

}

