using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public List<MiniMapIcon> staticIcons;

    public List<MiniMapIcon> dynamicIcons;

    [SerializeField]
    private RectTransform _miniMapImage;

    [SerializeField]
    private GameObject _iconsParentObject;

    
    private Transform _player;

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


    private void OnEnable()
    {
        UIManager.OnRefreshMiniMap += UIManager_OnRefreshMiniMap;
        GameManager.OnSendCharacter += GameManager_OnSendCharacter;
    }

    private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        _player = obj.transform;
    }

    private void UIManager_OnRefreshMiniMap()
    {
        for (int i = 0; i < staticIcons.Count; i++)
        {
            if (staticIcons[i].GetComponent<MiniMapIcon>().icon==null)
            {
                Destroy(staticIcons[i].gameObjectOnMiniMap);
                staticIcons.Remove(staticIcons[i]);
                
            }
        }
    }

    private void OnDisable()
    {
        UIManager.OnRefreshMiniMap -= UIManager_OnRefreshMiniMap;
        GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
    }
    void Start()
    {
       
        //ratio=CalculateRatio(mapLeftCorner3D.position,mapRightCorner3D.position,mapRightDownCorner3D.position,mapLeftCorner2D.localPosition,mapRightCorner2D.localPosition,mapRightDownCorner2D.localPosition);
        CalculateRatio(_mapLeftCorner3D.position,_mapRightCorner3D.position,_mapRightDownCorner3D.position,_mapLeftCorner2D.localPosition,_mapRightCorner2D.localPosition,_mapRightDownCorner2D.localPosition);
        //playerInMap.localPosition = player.transform.position * ratio;
        if (staticIcons.Count != 0)
        {
            SetStaticIcons();
        }



    }


    void Update()
    {
        //CalculateRatio(mapLeftCorner3D.position, mapRightCorner3D.position, mapRightDownCorner3D.position, mapLeftCorner2D.localPosition, mapRightCorner2D.localPosition, mapRightDownCorner2D.localPosition);
        _miniMapImage.localPosition = new Vector2(-_player.position.x * (_ratioX), -_player.position.z * (_ratioY));
        _playerIcon.transform.localEulerAngles =new Vector3(0,0, -_player.transform.eulerAngles.y) ;
        
    }




    private void CalculateRatio(Vector3 upLeftCorner,Vector3 upRightCorner,Vector3 rightDownCorner, Vector2 miniMapUpLeftCorner, Vector2 miniMapUpRightCorner, Vector2 miniMapRightDownCorner)
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
    public void SetStaticIcons()
    {
        for (int i = 0; i < staticIcons.Count; i++)
        {
            GameObject gO;
            gO = Instantiate(EmptyIcon);
            gO.transform.SetParent(_iconsParentObject.transform);
            gO.transform.position = Vector3.zero;
            gO.GetComponent<Image>().sprite = staticIcons[i].icon;
            staticIcons[i].gameObjectOnMiniMap = gO;
            gO.transform.localPosition = new Vector2(staticIcons[i].realWorldPos.position.x*_ratioX, staticIcons[i].realWorldPos.position.z*_ratioY);

            
        }
    }
}
