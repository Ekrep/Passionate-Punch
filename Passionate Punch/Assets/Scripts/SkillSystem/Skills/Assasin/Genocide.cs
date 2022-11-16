using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genocide : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private List<GameObject> _assasinSilhouettes;

    [SerializeField]
    private ParticleSystem _damageCircleParticle;

    [SerializeField]
    private ParticleSystem _lightningParticle;

    [SerializeField]
    private ParticleSystem _slashParticle;

    [SerializeField]
    private List<ParticleSystem> _silhoutteElectricityParticle;

    [SerializeField]
    private List<ParticleSystem> _silhouttePowerDrawParticle;

    [SerializeField]
    private MeshRenderer _ambiance;
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

    [Header("Camera")]
    [SerializeField]
    private float _camShakeRange;


    void Start()
    {

        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", -1);
        }
        _ambiance.material.SetFloat("_Dissolve", 0.85f); 





    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CreateAmbiance(_ambianceCreationSpeed));
        }
    }

    IEnumerator CastSkillEffects()
    {
        _damageCircleParticle.Play();
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _silhouttePowerDrawParticle[i].Play();
            _silhoutteElectricityParticle[i].Play();
            
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha") != 0.3f)
            {
                GameManager.Instance.ShakeCam(_camShakeRange);
                alphaValue = Mathf.MoveTowards(alphaValue, 0.3f, _silhouetteAlphaIncreaseSpeed * Time.deltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
                yield return new WaitForEndOfFrame();
                if (alphaValue == 0.3f)
                {

                    yield return new WaitForSeconds(_silhoutteCreationDelayTime);
                    _slashParticle.Stop();
                    if (i != _assasinSilhouettes.Count - 1)
                    {
                        float angle = Mathf.Atan2((-_assasinSilhouettes[i].transform.position.x + _assasinSilhouettes[i + 1].transform.position.x), -_assasinSilhouettes[i].transform.position.z + _assasinSilhouettes[i + 1].transform.position.z) * Mathf.Rad2Deg;
                        _slashParticle.transform.SetPositionAndRotation(new Vector3(_assasinSilhouettes[i].transform.position.x, _assasinSilhouettes[i].transform.position.y + 1, _assasinSilhouettes[i].transform.position.z), Quaternion.Euler(0, angle, 0));
                        _slashParticle.Play();
                      
                    }


                }
            }
        }
        GameManager.Instance.StopShakeCam();
        yield return new WaitForSeconds(_silhouetteDissappearDelay);
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _silhoutteElectricityParticle[i].Stop();
            _silhouttePowerDrawParticle[i].Stop();
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha") != -1)
            {
                alphaValue = Mathf.MoveTowards(alphaValue, -1f, _silhouetteAlphaIncreaseSpeed * Time.deltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
                yield return new WaitForEndOfFrame();

            }
            _assasinSilhouettes[i].SetActive(false);

        }
    }
    IEnumerator CreateAmbiance(float ambianceCreationSpeed)
    {
        LightManager.Instance.DarkAfterEnlight(_darkenSpeed, _enlightDelay);
        _lightningParticle.Play();
        float ambianceCreationValue = 0.85f;
        _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
        while (_ambiance.material.GetFloat("_Dissolve") != 0)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0, ambianceCreationSpeed * Time.deltaTime);
            _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CastSkillEffects());
        yield return new WaitForSeconds(_ambianceDissappearDelay);

        while (_ambiance.material.GetFloat("_Dissolve") != 0.85f)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0.85f, ambianceCreationSpeed * Time.deltaTime);
            _ambiance.material.SetFloat("_Dissolve", ambianceCreationValue);
            yield return new WaitForEndOfFrame();
        }
        _lightningParticle.Stop();

    }
}



