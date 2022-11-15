using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genocide : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private List<GameObject> _assasinSilhouettes;

    [SerializeField]
    private ParticleSystem _lightningParticle;

    public MeshRenderer _ambiance;
    [Header("Light")]
    [SerializeField]
    private float _darkenSpeed;

    [Header("Assasin Silhouette")]
    [SerializeField]
    private float _silhouetteAlphaIncreaseSpeed;

    [SerializeField]
    private float _silhoutteCreationDelayTime;

    [SerializeField]
    private float _silhouetteDissappearDelay;

    [Header("Ambiance")]
    [SerializeField]
    private float _ambianceCreationSpeed;

    [SerializeField]
    private float _ambianceDissappearDelay;

    [Header("Light")]
    [SerializeField]
    private float _enlightDelay;


    void Start()
    {
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", -1);
        }
        
        
        StartCoroutine(CastSkillEffects());
        StartCoroutine(CreateAmbiance(_ambianceCreationSpeed));

    }

    
    IEnumerator CastSkillEffects()
    {
        
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha")!=0.3f)
            {
                alphaValue = Mathf.MoveTowards(alphaValue, 0.3f,_silhouetteAlphaIncreaseSpeed*Time.deltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
                yield return new WaitForEndOfFrame();
                if (alphaValue==0.3f)
                {
                    yield return new WaitForSeconds(_silhoutteCreationDelayTime);

                }
            }
        }
        yield return new WaitForSeconds(_silhouetteDissappearDelay);
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha") != -1)
            {
                alphaValue = Mathf.MoveTowards(alphaValue, -1f, _silhouetteAlphaIncreaseSpeed * Time.deltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
                yield return new WaitForEndOfFrame();
               
            }

        }
    }
    IEnumerator CreateAmbiance(float ambianceCreationSpeed)
    {
        LightManager.Instance.DarkenedWorld(_darkenSpeed,_enlightDelay);
        float ambianceCreationValue = 0.85f;
        _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
        while (_ambiance.material.GetFloat("_Dissolve")!=0)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0, ambianceCreationSpeed*Time.deltaTime);
            _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(_ambianceDissappearDelay);
        while (_ambiance.material.GetFloat("_Dissolve") != 0.85f)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0.85f, ambianceCreationSpeed * Time.deltaTime);
            _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
            yield return new WaitForEndOfFrame();
        }

    }
}


