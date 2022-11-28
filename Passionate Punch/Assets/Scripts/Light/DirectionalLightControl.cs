using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightControl : MonoBehaviour
{
    [SerializeField]
    private Light _mainLight;

    private void OnEnable()
    {
        LightManager.OnDarkAfterEnlight += LightManager_OnDarkAfterEnlight;
    }

    private void LightManager_OnDarkAfterEnlight(float arg1, float arg2)
    {
        DarkenedWorld(arg1, arg2);
    }

    private void OnDisable()
    {
        LightManager.OnDarkAfterEnlight -= LightManager_OnDarkAfterEnlight;
    }



    public void DarkenedWorld(float darkenWorldSpeed, float enlightDelay)
    {


        StartCoroutine(DarkWorld(darkenWorldSpeed, enlightDelay));


    }
    IEnumerator DarkWorld(float darkenWorldSpeed, float enlightDelay)
    {
        Color firstColor = _mainLight.color;
        float r = _mainLight.color.r;
        float g = _mainLight.color.g;
        float b = _mainLight.color.b;
        while (_mainLight.color != new Color(0, 0, 0))
        {
            yield return new WaitForEndOfFrame();
            r = Mathf.MoveTowards(r, 0, darkenWorldSpeed * Time.deltaTime);
            g = Mathf.MoveTowards(g, 0, darkenWorldSpeed * Time.deltaTime);
            b = Mathf.MoveTowards(b, 0, darkenWorldSpeed * Time.deltaTime);
            _mainLight.color = new Color(r, g, b);
        }
        yield return new WaitForSeconds(enlightDelay);
        while (_mainLight.color != firstColor)
        {
            yield return new WaitForEndOfFrame();
            r = Mathf.MoveTowards(r, firstColor.r, darkenWorldSpeed * Time.deltaTime);
            g = Mathf.MoveTowards(g, firstColor.g, darkenWorldSpeed * Time.deltaTime);
            b = Mathf.MoveTowards(b, firstColor.b, darkenWorldSpeed * Time.deltaTime);
            _mainLight.color = new Color(r, g, b);
        }
    }
}
