using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public enum IconType
    {
        Static,
        Dynamic
    }

    public Sprite icon;
    [HideInInspector]
    public Transform realWorldPos;

    [HideInInspector]
    public GameObject gameObjectOnMiniMap;

    public IconType iconType;
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        realWorldPos = gameObject.transform;
        switch (iconType)
        {
            case IconType.Static:
                UIManager.Instance.miniMap.staticIcons.Add(this);
                break;
            case IconType.Dynamic:
                UIManager.Instance.miniMap.dynamicIcons.Add(this);
                break;
            default:
                break;
        }
    }


    public void DisableIcon()
    {
        for (int i = 0; i < UIManager.Instance.miniMap.staticIcons.Count; i++)
        {
            if (UIManager.Instance.miniMap.staticIcons[i]==this)
            {
                UIManager.Instance.miniMap.staticIcons[i].icon = null;
                UIManager.Instance.RefreshMiniMap();
                break;
            }
        }
    }
}
