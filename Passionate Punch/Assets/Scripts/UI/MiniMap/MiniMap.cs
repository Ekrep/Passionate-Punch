using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }




    private float CalculateRatio(Vector3 upLeftCorner,Vector3 upRightCorner,Vector3 rightDownCorner, Vector2 miniMapUpLeftCorner, Vector2 miniMapUpRightCorner, Vector2 miniMapRightDownCorner)
    {
        float upwardsDistance;
        float sideDistance;

        float miniMapUpwardsDistance;
        float miniMapSideDistance;

        float miniMapArea;
        float realWorldArea;


        upwardsDistance = Vector3.Distance(upLeftCorner, upRightCorner);
        sideDistance = Vector3.Distance(upRightCorner, rightDownCorner);

        miniMapUpwardsDistance = Vector2.Distance(miniMapUpLeftCorner, miniMapUpRightCorner);
        miniMapSideDistance = Vector2.Distance(miniMapUpRightCorner, miniMapRightDownCorner);

        realWorldArea = upwardsDistance * sideDistance;
        miniMapArea = miniMapUpwardsDistance*miniMapSideDistance;

        return miniMapArea / realWorldArea;






    }
}
