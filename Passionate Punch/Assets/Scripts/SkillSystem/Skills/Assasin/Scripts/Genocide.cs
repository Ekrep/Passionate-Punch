using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Interfaces;

public class Genocide : MonoBehaviourSkill
{
    //karakter mat fresnel yap slash yollas?n yere sonra alpha k?s biti?te alpha a? karakter material'i s?f?rla
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

    private List<EnemyMovementSM> _nestedEnemies=new List<EnemyMovementSM>();


    

    //true when skill damage
    private bool canHit;

    private void OnDisable()
    {
        
        _silhoutteMat.SetFloat("_Alpha", 0);
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", -1f);
            _assasinSilhouettes[i].SetActive(true);
        }
        _dagger.SetActive(false);
        _nestedEnemies.Clear();
    }
    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }
    private void OnEnable()
    {
        
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", -2f);
        }
        _ambiance.sharedMaterial.SetFloat("_Dissolve", 0.85f);
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(skillSettings.Character.transform.position.x, skillSettings.Character.transform.position.y, skillSettings.Character.transform.position.z-1f), Quaternion.identity);
    }

    private void Start()
    {
         
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyMovementSM damagables))
        {
            _nestedEnemies.Add(damagables);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        //stun action begins
        if (other.gameObject.TryGetComponent(out IHealth damagables)&&other.gameObject.TryGetComponent(out EnemyMovementSM enemyState))
        {
            if (enemyState.currentEnemyState!=enemyState.enemyStunState&&enemyState.currentEnemyState!=enemyState.enemyDieState)
            {
                damagables.Hit(SkillSystem.SkillSettings.HitType.Hard, 0, Vector3.zero, 0);
            }
            
        }
    }

    public override void Cast()
    {
        GameManager.Instance.SkillCasted(skillSettings.manaCost);
        Timing.RunCoroutine(CreateAmbiance(_ambianceCreationSpeed));
        skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
        skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
        Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
        skillSettings.canCast = false;
        Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        yield return  Timing.WaitForSeconds(time);
        while (skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha") != 0.3f)
        {
            float alphaValue = skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha");
            alphaValue = Mathf.MoveTowards(alphaValue, 0.3f, _characterDissappearSpeed*1.5f * Timing.DeltaTime);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", alphaValue);
            yield return Timing.WaitForOneFrame;
        }
        //weapons on characters hands 
        for (int i = 0; i < skillSettings.Character.characterWeapons.Count; i++)
        {
            skillSettings.Character.characterWeapons[i].SetActive(true);
        }
        skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = _firstMat;
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);
        gameObject.SetActive(false);

    }

    public override IEnumerator<float> Cooldown(float time)
    {
        yield return Timing.WaitForSeconds(time);
        skillSettings.canCast = true;
        
    }

    IEnumerator<float> CastSkillEffects()
    {
        _damageCircleParticle.Play();
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _silhouttePowerDrawParticle[i].Play();
            _silhoutteElectricityParticle[i].Play();
            
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha") != 0.3f)
            {
                GameManager.Instance.ShakeCam(_camShakeRange);
                alphaValue = Mathf.MoveTowards(alphaValue, 0.3f, _silhouetteAlphaIncreaseSpeed * Timing.DeltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", alphaValue);
                yield return Timing.WaitForOneFrame;
                if (alphaValue == 0.3f)
                {

                    yield return Timing.WaitForSeconds(_silhoutteCreationDelayTime);
                    _slashParticle.Stop();
                    if (i != _assasinSilhouettes.Count - 1)
                    {
                        float angle = Mathf.Atan2((-_assasinSilhouettes[i].transform.position.x + _assasinSilhouettes[i + 1].transform.position.x), -_assasinSilhouettes[i].transform.position.z + _assasinSilhouettes[i + 1].transform.position.z) * Mathf.Rad2Deg;
                        _slashParticle.transform.SetPositionAndRotation(new Vector3(_assasinSilhouettes[i].transform.position.x, _assasinSilhouettes[i].transform.position.y + 1, _assasinSilhouettes[i].transform.position.z), Quaternion.Euler(0, angle, 0));
                        _slashParticle.Play();
                        for (int j = 0; j < _nestedEnemies.Count; j++)
                        {
                            if (_nestedEnemies[j]!=null&&_nestedEnemies[j].currentEnemyState!=_nestedEnemies[j].enemyDieState)
                            {
                                //hit action begins
                                _nestedEnemies[j].Hit(SkillSystem.SkillSettings.HitType.Low, skillSettings.skillDamage, Vector3.zero, 0);
                            }
                            
                        }
                      
                    }


                }
            }
        }
        GameManager.Instance.StopShakeCam();
        yield return Timing.WaitForSeconds(_silhouetteDissappearDelay);
        for (int i = 0; i < _assasinSilhouettes.Count; i++)
        {
            _silhoutteElectricityParticle[i].Stop();
            _silhouttePowerDrawParticle[i].Stop();
            float alphaValue = _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha");
            while (_assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha") != -1)
            {
                alphaValue = Mathf.MoveTowards(alphaValue, -1f, _silhouetteAlphaIncreaseSpeed * Timing.DeltaTime);
                _assasinSilhouettes[i].GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", alphaValue);
                yield return  Timing.WaitForOneFrame;

            }
            _assasinSilhouettes[i].SetActive(false);

        }
    }
    IEnumerator<float> CreateAmbiance(float ambianceCreationSpeed)
    {

        //weapons on characters hands 
        for (int i = 0; i < skillSettings.Character.characterWeapons.Count; i++)
        {
            skillSettings.Character.characterWeapons[i].SetActive(false);
        }
        skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = _silhoutteMat;
        while (skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha") != -2f)
        {
            float alphaValue = skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.GetFloat("_Alpha");
            alphaValue = Mathf.MoveTowards(alphaValue, -2f, _characterDissappearSpeed * Timing.DeltaTime);
            skillSettings.Character.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetFloat("_Alpha", alphaValue);
            yield return Timing.WaitForOneFrame;
            


        }
       
        _dagger.SetActive(true);
        
        
        yield return Timing.WaitForSeconds(_ambianceCreationDelay);
        LightManager.Instance.DarkAfterEnlight(_darkenSpeed, _enlightDelay);
        _lightningParticle.Play();
        float ambianceCreationValue = 0.85f;
        _ambiance.sharedMaterial.SetFloat("_Dissolve", ambianceCreationValue);
        while (_ambiance.sharedMaterial.GetFloat("_Dissolve") != 0)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0, ambianceCreationSpeed * Timing.DeltaTime);
            _ambiance.sharedMaterial.SetFloat("_Dissolve", ambianceCreationValue);
            yield return Timing.WaitForOneFrame;
        }
        Timing.RunCoroutine(CastSkillEffects());
        yield return Timing.WaitForSeconds(_ambianceDissappearDelay);

        while (_ambiance.sharedMaterial.GetFloat("_Dissolve") != 0.85f)
        {
            ambianceCreationValue = Mathf.MoveTowards(ambianceCreationValue, 0.85f, ambianceCreationSpeed * Timing.DeltaTime);
            _ambiance.sharedMaterial.SetFloat("_Dissolve", ambianceCreationValue);
            yield return Timing.WaitForOneFrame;
        }
        _lightningParticle.Stop();

       

    }

 
}



