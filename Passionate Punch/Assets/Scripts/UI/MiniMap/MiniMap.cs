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
        CalculateRatio(mapLeftCorner3D.position,mapRightCorner3D.position,mapRightDownCorner3D.position,mapLeftCorner2D.localPosition,mapRightCorner2D.localPosition,mapRightDownCorner2D.localPosition);
        //playerInMap.localPosition = player.transform.position * ratio;
        


    }


    void Update()
    {
        //CalculateRatio(mapLeftCorner3D.position, mapRightCorner3D.position, mapRightDownCorner3D.position, mapLeftCorner2D.localPosition, mapRightCorner2D.localPosition, mapRightDownCorner2D.localPosition);
        playerInMap.localPosition = new Vector2(-player.position.x * (ratioX), -player.position.z * (ratioY));
        
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

        ratioX = miniMapXLenght / realWorldXLenght;
        ratioY = miniMapYLenght / realWorldYLenght;





    }
}
