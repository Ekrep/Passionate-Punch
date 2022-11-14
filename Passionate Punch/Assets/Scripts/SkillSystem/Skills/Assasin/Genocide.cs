using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genocide : MonoBehaviour
{
    public List<GameObject> assasinSilhouettes;

    public MeshRenderer ambiance;

    [SerializeField]
    private float _darkenSpeed;

    [SerializeField]
    private float _silhouetteAlphaIncreaseSpeed;

    [SerializeField]
    private float _silhoutteCreationDelayTime;
    
    void Start()
    {
        for (int i = 0; i < assasinSilhouettes.Count; i++)
        {
            assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", -1);
        }
        //ambiance.material.SetFloat("_Dissolve", 0);
        LightManager.Instance.DarkenedWorld(_darkenSpeed);
        StartCoroutine(CastSkillEffects());

    }

    
    IEnumerator CastSkillEffects()
    {
        
        for (int i = 0; i < assasinSilhouettes.Count; i++)
        {
            float alphaValue = assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            while (assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha")!=0.75f)
            {
                alphaValue = Mathf.MoveTowards(alphaValue, 0.75f,_silhouetteAlphaIncreaseSpeed);
                assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
                yield return new WaitForEndOfFrame();
                if (alphaValue==0.75f)
                {
                    yield return new WaitForSeconds(_silhoutteCreationDelayTime);
                }
            }
        }
    }
}



