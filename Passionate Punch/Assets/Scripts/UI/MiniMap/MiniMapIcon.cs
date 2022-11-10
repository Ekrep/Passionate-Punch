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

    public Sprite Icon;
    [HideInInspector]
    public Transform realWorldPos;

    public IconType iconType;

    private void Start()
    {

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
}
