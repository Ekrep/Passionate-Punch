using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Decal : MonoBehaviour
{
    public DecalScriptable decalSettings;
 
    public DecalProjector circleProjector;
  
    public DecalProjector indicatorProjector;

    public GameObject parent;


    public float decalHeight;
    public float decalWidth;



    public virtual void SetDecalImages()
    {
        circleProjector.material.SetTexture("Base_Map", decalSettings.circleRangeProjectorTexture);
        indicatorProjector.material.SetTexture("Base_Map", decalSettings.indicatorProjectorTexture);
        circleProjector.size = new Vector3(decalWidth, decalHeight, circleProjector.size.z);
        indicatorProjector.size= new Vector3(decalWidth/2f, decalHeight/2f, indicatorProjector.size.z);
    }



    public abstract void SetDecalPosAndRot(Vector3 decaltargetPosition, Vector3 lookPos);
    


        //indicatorProjector.transform.position = decaltargetPosition;
        //float angle=0f;
        //angle = Mathf.Atan2(lookPos.x, lookPos.y)*Mathf.Rad2Deg;
        //indicatorProjector.transform.rotation = Quaternion.Euler(indicatorProjector.transform.rotation.x, angle, indicatorProjector.transform.rotation.z);
      

    
}
