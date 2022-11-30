using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Interfaces;

public class LandMine : MonoBehaviour
{
    [HideInInspector]
    public ExplosiveTrap parentSkill;

    [SerializeField]
    private ParticleSystem _explodeParticle;

    private bool _isTriggered;

    [SerializeField]
    private MeshRenderer _warningFresnelMat;

    [SerializeField]
    private float _signSpeed;

    private List<CoroutineHandle> coroutineHandles = new List<CoroutineHandle>();

    private void OnDisable()
    {
        _isTriggered = false;
        for (int i = 0; i < coroutineHandles.Count; i++)
        {
           Timing.KillCoroutines(coroutineHandles[i]);
        }
        _warningFresnelMat.material.SetFloat("_Alpha", 0.1f);
    }
    private void OnEnable()
    {
        CoroutineHandle corot1;
        CoroutineHandle corot2;
        corot1=Timing.RunCoroutine(DestroySelf());
        corot2=Timing.RunCoroutine(WarningSign());
        coroutineHandles.Add(corot1);
        coroutineHandles.Add(corot2);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHealth enemy)&&!_isTriggered)
        {
            Timing.RunCoroutine(Hit());
            _isTriggered = true;
        }
    }

    IEnumerator<float> WarningSign()
    {
        bool increase=false;
        while (gameObject.activeSelf)
        {
            if (increase)
            {
                float value = _warningFresnelMat.material.GetFloat("_Alpha");
                value = Mathf.MoveTowards(value, -0.3f, _signSpeed);
                _warningFresnelMat.material.SetFloat("_Alpha", value);
                if (value<=-0.3f)
                {
                    increase = false;
                }
                
            }
            else
            {
                float value = _warningFresnelMat.material.GetFloat("_Alpha");
                value = Mathf.MoveTowards(value, 0.1f, _signSpeed);
                _warningFresnelMat.material.SetFloat("_Alpha", value);
                if (value>=0.1f)
                {
                    increase = true;
                }
                
            }
            
            
            yield return Timing.WaitForOneFrame;
        }
       

    }

    IEnumerator<float> DestroySelf()
    {
        yield return Timing.WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
   

    IEnumerator<float> Hit()
    {
        Collider[] colliders = new Collider[10];
        int count = 0;
        count = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 2, colliders);
        Debug.Log(colliders.Length);
        for (int i = 0; i < count; i++)
        {
            if (colliders[i].TryGetComponent<IHealth>(out IHealth enemyHealth))
            {
                //needs vec3
                _explodeParticle.Play();
                enemyHealth.Hit(parentSkill.skillSettings.hitType, parentSkill.skillSettings.skillDamage, colliders[i].gameObject.transform.forward * -1, 1500f);

            }



        }
        yield return Timing.WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
