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
  public void DarkenedWorld(float darkenWorldSpeed, float enlightDelay)
  {

       
        StartCoroutine(DarkWorld(darkenWorldSpeed,enlightDelay));
        

  }
    IEnumerator DarkWorld(float darkenWorldSpeed, float enlightDelay)
    {
        Color firstColor = mainLight.color;
        float r = mainLight.color.r;
        float g = mainLight.color.g;
        float b = mainLight.color.b;
        while (mainLight.color!=new Color(0,0,0))
        {
            yield return new WaitForEndOfFrame();
            r = Mathf.MoveTowards(r, 0,darkenWorldSpeed*Time.deltaTime);
            g = Mathf.MoveTowards(g, 0, darkenWorldSpeed * Time.deltaTime);
            b = Mathf.MoveTowards(b, 0, darkenWorldSpeed * Time.deltaTime);
            mainLight.color = new Color(r, g, b);
            Debug.Log("girdimcorotlight");
        }
        yield return new WaitForSeconds(enlightDelay);
        while (mainLight.color != firstColor)
        {
            yield return new WaitForEndOfFrame();
            r = Mathf.MoveTowards(r, firstColor.r, darkenWorldSpeed * Time.deltaTime);
            g = Mathf.MoveTowards(g, firstColor.g, darkenWorldSpeed * Time.deltaTime);
            b = Mathf.MoveTowards(b, firstColor.b, darkenWorldSpeed * Time.deltaTime);
            mainLight.color = new Color(r, g, b);
            Debug.Log("girdimcorotlight");
        }
    }
}
