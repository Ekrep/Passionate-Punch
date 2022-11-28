using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStormDecal : Decal
{


    private void Start()
    {
        SetDecalImages();
        indicatorProjector.transform.localPosition = new Vector3(indicatorProjector.transform.localPosition.x, indicatorProjector.transform.localPosition.y, indicatorProjector.transform.localPosition.z +0.4f);
    }
    public override void SetDecalPosAndRot(Vector3 decaltargetPosition, Vector3 lookPos)
    {    
        float angle=0f;
        angle = Mathf.Atan2(lookPos.x, lookPos.y)*Mathf.Rad2Deg;
        parent.transform.rotation = Quaternion.Euler(0, angle, 0);


    }


}
