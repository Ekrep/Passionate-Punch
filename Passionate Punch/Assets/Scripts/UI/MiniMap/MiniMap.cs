using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class MiniMap : MonoBehaviour
{
    [Range(0.02f,0.2f)]
    [SerializeField]
    private float _miniMapRefreshTime;

    public List<MiniMapIcon> staticIcons;

    public List<MiniMapIcon> dynamicIcons;

    private List<GameObject> _movingIncons = new List<GameObject>();

    [SerializeField]
    private RectTransform _miniMapImage;

    [SerializeField]
    private GameObject _iconsParentObject;

    private Transform _Player
    {
        get
        {
            return GameManager.Instance.character.transform;
        }
    }

    [SerializeField]
    private RectTransform _playerIcon;

    [SerializeField]
    private Transform _mapLeftCorner3D;
    [SerializeField]
    private Transform _mapRightCorner3D;
    [SerializeField]
    private Transform _mapRightDownCorner3D;

    [SerializeField]
    private Transform _mapLeftCorner2D;
    [SerializeField]
    private Transform _mapRightCorner2D;
    [SerializeField]
    private Transform _mapRightDownCorner2D;

    private float _ratioX;

    private float _ratioY;

    public GameObject EmptyIcon;

    private CoroutineHandle _updateMiniMapCoroutine;

    private void Awake()
    {
        CalculateRatio(_mapLeftCorner3D.position, _mapRightCorner3D.position, _mapRightDownCorner3D.position, _mapLeftCorner2D.localPosition, _mapRightCorner2D.localPosition, _mapRightDownCorner2D.localPosition);
    }
    private void OnEnable()
    {
       
        UIManager.OnRefreshMiniMap += UIManager_OnRefreshMiniMap;
        UIManager.OnCreateIcons += UIManager_OnCreateIcons;

    }

    private void UIManager_OnCreateIcons(MiniMapIcon obj)
    {
        switch (obj.iconType)
        {
            case MiniMapIcon.IconType.Static:
                staticIcons.Add(obj);
                SetStaticIcons(obj);
                break;
            case MiniMapIcon.IconType.Dynamic:
                dynamicIcons.Add(obj);
                CreateDynamicIncons(obj);
                break;
            default:
                break;
        }
    }

    private void UIManager_OnRefreshMiniMap(MiniMapIcon obj)
    {
        switch (obj.iconType)
        {
            case MiniMapIcon.IconType.Static:
                for (int i = 0; i < staticIcons.Count; i++)
                {
                    if (staticIcons[i].icon == null)
                    {
                        Destroy(staticIcons[i].gameObjectOnMiniMap);
                        staticIcons.Remove(staticIcons[i]);

                    }
                }
                break;
            case MiniMapIcon.IconType.Dynamic:
                for (int i = 0; i < dynamicIcons.Count; i++)
                {
                    if (dynamicIcons[i].icon == null)
                    {
                        Destroy(dynamicIcons[i].gameObjectOnMiniMap);
                        dynamicIcons.Remove(dynamicIcons[i]);

                    }

                }
                break;
            default:
                break;
        }
       
       
    }

    private void OnDisable()
    {
        UIManager.OnRefreshMiniMap -= UIManager_OnRefreshMiniMap;
        UIManager.OnCreateIcons -= UIManager_OnCreateIcons;
        Timing.KillCoroutines(_updateMiniMapCoroutine);

    }
    void Start()
    { 
        _updateMiniMapCoroutine=Timing.RunCoroutine(UpdateMiniMap(_miniMapRefreshTime));  
    }


    IEnumerator<float> UpdateMiniMap(float refreshRate)
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(refreshRate);
            _miniMapImage.localPosition = new Vector2(-_Player.position.x * (_ratioX), -_Player.position.z * (_ratioY));
            _playerIcon.transform.localEulerAngles = new Vector3(0, 0, -_Player.eulerAngles.y);
            SetDynamicIcons();
        }
    }

    private void CalculateRatio(Vector3 upLeftCorner, Vector3 upRightCorner, Vector3 rightDownCorner, Vector2 miniMapUpLeftCorner, Vector2 miniMapUpRightCorner, Vector2 miniMapRightDownCorner)
    {
        float realWorldXLenght;
        realWorldXLenght = Vector3.Distance(upLeftCorner, upRightCorner);//Mathf.Abs(upLeftCorner.x - upRightCorner.x);
        float realWorldYLenght;
        realWorldYLenght = Vector3.Distance(upRightCorner, rightDownCorner);//Mathf.Abs(upRightCorner.z - rightDownCorner.z);

        float miniMapXLenght;
        miniMapXLenght = Vector2.Distance(miniMapUpLeftCorner, miniMapUpRightCorner);//Mathf.Abs(miniMapUpLeftCorner.x - miniMapUpRightCorner.x);
        float miniMapYLenght;
        miniMapYLenght = Vector2.Distance(miniMapUpRightCorner, miniMapRightDownCorner);//Mathf.Abs(miniMapUpRightCorner.y - miniMapRightDownCorner.y);

        _ratioX = miniMapXLenght / realWorldXLenght;
        _ratioY = miniMapYLenght / realWorldYLenght;





    }
    public void SetStaticIcons(MiniMapIcon miniMapIcon)
    {
        GameObject gO;
        gO = Instantiate(EmptyIcon);
        gO.transform.SetParent(_iconsParentObject.transform);
        gO.transform.position = Vector3.zero;
        gO.TryGetComponent(out Image image);
        image.sprite = miniMapIcon.icon;
        image.SetNativeSize();
        miniMapIcon.gameObjectOnMiniMap = gO;
        gO.transform.localPosition = new Vector2(miniMapIcon.realWorldPos.position.x * _ratioX, miniMapIcon.realWorldPos.position.z * _ratioY);



    }

    private void CreateDynamicIncons(MiniMapIcon miniMapIcon)
    {

        GameObject gO;
        gO = Instantiate(EmptyIcon);
        gO.transform.SetParent(_iconsParentObject.transform);
        gO.transform.localPosition = Vector3.zero;
        gO.TryGetComponent(out Image image);
        image.sprite = miniMapIcon.icon;
        image.SetNativeSize();
        miniMapIcon.gameObjectOnMiniMap = gO;
        gO.transform.localPosition = new Vector2(miniMapIcon.realWorldPos.position.x * _ratioX, miniMapIcon.realWorldPos.position.z * _ratioY);
      


    }
    private void SetDynamicIcons()
    {
        for (int i = 0; i < dynamicIcons.Count; i++)
        {

            dynamicIcons[i].gameObjectOnMiniMap.transform.localPosition = new Vector2(dynamicIcons[i].realWorldPosDynamic.position.x * _ratioX, dynamicIcons[i].realWorldPosDynamic.position.z * _ratioY);

        }
    }
}
