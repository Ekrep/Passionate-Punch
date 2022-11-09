using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public RectTransform playerInMap;
    public Transform player;

    public Transform mapLeftCorner3D;
    public Transform mapRightCorner3D;
    public Transform mapRightDownCorner3D;

    public Transform mapLeftCorner2D;
    public Transform mapRightCorner2D;
    public Transform mapRightDownCorner2D;

    public float ratioX;

    public float ratioY;
    void Start()
    {
        //ratio=CalculateRatio(mapLeftCorner3D.position,mapRightCorner3D.position,mapRightDownCorner3D.position,mapLeftCorner2D.localPosition,mapRightCorner2D.localPosition,mapRightDownCorner2D.localPosition);
        CalculateRatio(mapLeftCorner3D.position,mapRightCorner3D.position,mapRightDownCorner3D.position,mapLeftCorner2D.position,mapRightCorner2D.position,mapRightDownCorner2D.position);
        //playerInMap.localPosition = player.transform.position * ratio;
        


    }


    void Update()
    {
        playerInMap.localPosition = new Vector2(player.position.x * 1/ratioX, player.position.z * 1/ratioY);
    }




    private void CalculateRatio(Vector3 upLeftCorner,Vector3 upRightCorner,Vector3 rightDownCorner, Vector2 miniMapUpLeftCorner, Vector2 miniMapUpRightCorner, Vector2 miniMapRightDownCorner)
    {
        float realWorldXLenght;
        realWorldXLenght = Mathf.Abs(upLeftCorner.x - upRightCorner.x);
        float realWorldYLenght;
        realWorldYLenght = Mathf.Abs(upRightCorner.z - rightDownCorner.z);

        float miniMapXLenght;
        miniMapXLenght = Mathf.Abs(miniMapUpLeftCorner.x - miniMapUpRightCorner.x);
        float miniMapYLenght;
        miniMapYLenght = Mathf.Abs(miniMapUpRightCorner.y - miniMapRightDownCorner.y);

        ratioX = realWorldXLenght / miniMapXLenght;
        ratioY = realWorldYLenght / miniMapYLenght;





    }
}
