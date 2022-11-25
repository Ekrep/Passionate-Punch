using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnImagePressed()
    {
        if(this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else {
            this.gameObject.SetActive(true);
        }
        
    }
}
