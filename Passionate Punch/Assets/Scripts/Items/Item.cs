using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using TMPro;
using UnityEngine.UI;


namespace Items
{
    public class Item : MonoBehaviour
    {
        //The purpose of this class is to handle various functionalities that items will have apart form data class. 
        public ItemSettings itemSettings;
        public Transform player;

        private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
        {
            player = obj.gameObject.transform;
        }
        void Start()
        {
            GetComponent<MeshFilter>().mesh = itemSettings.itemMesh;
            GetComponent<MeshRenderer>().material = itemSettings.itemMaterial;
        }

        void Update()
        {
            //CheckDistance();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Inventory>() != null)
            {
                UIManager.Instance.TriggeredWithItem();
            }
        }

        void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.GetComponent<Inventory>() != null)
            {
                if (UIManager.Instance.isPickUpButtonPressed)
                {
                    
                    PickedUp(collider);
                }
            }

        }

        /*void CheckDistance()
        {
            if (player.position.x - this.transform.position.x < itemSettings.radius ||
            player.position.z - this.transform.position.z < itemSettings.radius)
            {
                itemText.GetComponent<TextMeshProUGUI>().text = itemSettings.itemTitle;
                itemText.SetActive(true);
            }
        }*/

        public void PickedUp(Collider collider)
        {
            if (collider.gameObject.GetComponent<Inventory>().AddItem(this.itemSettings))
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }

}

