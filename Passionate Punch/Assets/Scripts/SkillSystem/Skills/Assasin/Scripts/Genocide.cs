using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genocide : MonoBehaviourSkill
{
    //karakter mat fresnel yap slash yollasýn yere sonra alpha kýs bitiþte alpha aç karakter material'i sýfýrla
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
    private Material _silhoutteMat;

    [SerializeField]
    private Material _firstMat;

    [SerializeField]
    private MeshRenderer _ambiance;

    [SerializeField]
    private GameObject _dagger;

    [Header("Light")]
    [SerializeField]
    private float _darkenSpeed;

    [Header("Character")]
    [SerializeField]
    private float _characterDissappearSpeed;

    [Header("Assasin Silhouette")]
    [SerializeField]
    private float _silhouetteAlphaIncreaseSpeed;

    [SerializeField]
    private float _silhoutteCreationDelayTime;

    [SerializeField]
    private float _silhouetteDissappearDelay;

    [Header("Ambiance")]
    [SerializeField]
    private float _ambianceCreationDelay;

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

    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }
    void Start()
    {

        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", -1);
        }
        _ambiance.material.SetFloat("_Dissolve", 0.85f);
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y, skillSettings.Character.transform.position.z-1.5f), Quaternion.identity);

    }

   

    
    public override void Cast()
    {
        StartCoroutine(CreateAmbiance(_ambianceCreationSpeed));
        skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
        skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
        StartCoroutine(ExitCastState(skillSettings.castTime));
        skillSettings.canCast = false;
        StartCoroutine(Cooldown(skillSettings.coolDown));
    }

    public override IEnumerator RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator ExitCastState(float time)
    {
        yield return new WaitForSeconds(time);
        while (skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha") != 0.3f)
        {
            float alphaValue = skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            alphaValue = Mathf.MoveTowards(alphaValue, 0.3f, _characterDissappearSpeed*1.5f * Time.deltaTime);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
            yield return new WaitForEndOfFrame();
        }
        skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _firstMat;
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);

    }

    public override IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        skillSettings.canCast = true;
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
        
        skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _silhoutteMat;
        while (skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha") != -1)
        {
            float alphaValue = skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.GetFloat("_Alpha");
            alphaValue = Mathf.MoveTowards(alphaValue, -1, _characterDissappearSpeed * Time.deltaTime);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Alpha", alphaValue);
            yield return new WaitForEndOfFrame();
            


        }
        
        _dagger.SetActive(true);
        
        
        yield return new WaitForSeconds(_ambianceCreationDelay);
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



