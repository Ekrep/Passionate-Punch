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
                // 
                if (!UIManager.Instance.pickUpButton.gameObject.activeInHierarchy)
                {
                    UIManager.Instance.pickUpButton.gameObject.SetActive(true);
                }
                if (!UIManager.Instance.pickUpButton.interactable)
                {
                    UIManager.Instance.pickUpButton.interactable = true;
                }
                //
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Inventory inventory))
        {
            UIManager.Instance.TriggeredWithItem();

            if (UIManager.Instance.isPickUpButtonPressed)
            {
                PickedUp(other);
                UIManager.Instance.TriggerExitWithItem();
            }

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() == null)
        {
            UIManager.Instance.TriggerExitWithItem();
                //
                UIManager.Instance.pickUpButton.interactable = false;
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
                UIManager.Instance.isPickUpButtonPressed = false;
                Destroy(this.gameObject);
                
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }

}

