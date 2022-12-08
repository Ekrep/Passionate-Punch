using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    
    public List<RectTransform>safeAreaTransforms;
    void Start()
    {
        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;
        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;
        for (int i = 0; i < safeAreaTransforms.Count; i++)
        {
            safeAreaTransforms[i].anchorMin = anchorMin;
            safeAreaTransforms[i].anchorMax = anchorMax;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
