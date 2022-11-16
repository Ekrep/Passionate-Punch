using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

public abstract class MonoBehaviourSkill : MonoBehaviour
{
    public SkillSettings skillSettings;



    public abstract void Cast();

    public abstract IEnumerator RevertSkillEffect(float time);

    public abstract IEnumerator ExitCastState(float time);

    public abstract IEnumerator Cooldown(float time);
}
