using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    public Light mainLight;

    



    private void Awake()
    {
        Instance = this;
    }
  public void DarkenedWorld(float darkenWorldSpeed)
  {

       
        StartCoroutine(DarkWorld(darkenWorldSpeed));
        

  }
    IEnumerator DarkWorld(float darkenWorldSpeed)
    {
        float r = mainLight.color.r;
        float g = mainLight.color.g;
        float b = mainLight.color.b;
        while (mainLight.color!=new Color(0,0,0))
        {
            yield return new WaitForEndOfFrame();
            r = Mathf.MoveTowards(r, 0,darkenWorldSpeed);
            g = Mathf.MoveTowards(g, 0, darkenWorldSpeed);
            b = Mathf.MoveTowards(b, 0, darkenWorldSpeed);
            mainLight.color = new Color(r, g, b);
            Debug.Log("girdimcorotlight");
        }
    }
}
