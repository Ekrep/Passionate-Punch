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
    public Transform realWorldPosDynamic;

    [HideInInspector]
    public GameObject gameObjectOnMiniMap;

    public IconType iconType;

    private bool _isDisabled=false;

    private void OnEnable()
    {
        if (_isDisabled)
        {
            realWorldPosDynamic = gameObject.transform;
            realWorldPos = gameObject.transform;
            UIManager.Instance.CreateIcons(this);
            _isDisabled = false;
        }
        
    }
    private void OnDisable()
    {
        _isDisabled = true;
    }
    private void Update()
    {
        realWorldPosDynamic = gameObject.transform;
    }
    private void Start()
    {
        realWorldPosDynamic = gameObject.transform;
        realWorldPos = gameObject.transform;
        UIManager.Instance.CreateIcons(this);
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
