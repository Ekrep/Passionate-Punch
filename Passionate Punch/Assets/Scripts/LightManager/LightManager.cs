using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
   

    private void Awake()
    {
        Instance = this;
    }

    public static event Action<float,float> OnDarkAfterEnlight;


    //sets the world dark first, after that enlightens
    public void DarkAfterEnlight(float darkenWorldSpeed, float enlightDelay )
    {
        if (OnDarkAfterEnlight!=null)
        {
            OnDarkAfterEnlight(darkenWorldSpeed,enlightDelay);
        }
    }

 
}
