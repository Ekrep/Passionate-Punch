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

    
    private void OnEnable()
    {
        Timing.RunCoroutine(DestroySelf());
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHealth enemy))
        {
            Timing.RunCoroutine(Hit());
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
                enemyHealth.Hit(parentSkill.skillSettings.hitType, parentSkill.skillSettings.skillDamage, colliders[i].gameObject.transform.forward * -1, 15000f);

            }



        }
        yield return Timing.WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
