using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InventorySystem;

namespace Items
{
    public class Item : MonoBehaviour
    {
        //The purpose of this class is to handle various functionalities that items will have apart form data class. 
        [SerializeField] private ItemSettings itemSettings;
        [SerializeField] private Rigidbody rigidBody;
        public Rigidbody Rigidbody => rigidBody;
        
        void OnTriggerEnter(Collider collider)
        {
            collider.gameObject.GetComponent<Inventory>().AddItem(this.gameObject.GetComponent<Item>());         
        }

        public void PickedUp()
        {
            Destroy(this.gameObject);
        }
    }

}

