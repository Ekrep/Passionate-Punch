using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;


public abstract class MonoBehaviourSkill : MonoBehaviour
{
    public SkillSettings skillSettings;
   


    public abstract void Cast();

    public abstract IEnumerator<float> RevertSkillEffect(float time);

    public abstract IEnumerator<float> ExitCastState(float time);

    public abstract IEnumerator<float> Cooldown(float time);
}
