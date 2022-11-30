using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class ExplosiveTrap : MonoBehaviourSkill
{
    [SerializeField]
    private LandMine _minePrefab;

    private Queue<LandMine> _landMines = new Queue<LandMine>();

    private bool isQueueCreated;

    private void OnDestroy()
    {
        skillSettings.canCast = true;
    }
    private void Start()
    {

    }
    public override void Cast()
    {
        if (!isQueueCreated)
        {
            CreateQueue();
            isQueueCreated = true;
        }
        if (skillSettings.canCast)
        {
            LandMine spawnedLandMine = TakeLandMineFromQueue();
            spawnedLandMine.transform.position = skillSettings.Character.transform.position;
            spawnedLandMine.parentSkill = this;
            spawnedLandMine.gameObject.SetActive(true);
            GameManager.Instance.SkillCasted(skillSettings.manaCost);
            skillSettings.Character.ChangeState(skillSettings.Character.characterSkillCastState);
            skillSettings.Character.anim.SetBool(skillSettings.animationName, true);
            Timing.RunCoroutine(ExitCastState(skillSettings.castTime));
            Timing.RunCoroutine(Cooldown(skillSettings.coolDown));
            skillSettings.canCast = false;
        }
    }

    public override IEnumerator<float> Cooldown(float time)
    {
        yield return Timing.WaitForSeconds(skillSettings.coolDown);
        skillSettings.canCast = true;
    }

    public override IEnumerator<float> ExitCastState(float time)
    {
        yield return Timing.WaitForSeconds(skillSettings.castTime);
        gameObject.SetActive(false);
        skillSettings.Character.anim.SetBool(skillSettings.animationName, false);
        skillSettings.Character.ChangeState(skillSettings.Character.characterIdleState);

    }

    public override IEnumerator<float> RevertSkillEffect(float time)
    {
        throw new System.NotImplementedException();
    }

    private LandMine TakeLandMineFromQueue()
    {
        LandMine poppedMine;
        poppedMine = _landMines.Dequeue();
        _landMines.Enqueue(poppedMine);
        return poppedMine;
    }

    private void CreateQueue()
    {
        for (int i = 0; i < 10; i++)
        {
            LandMine holder;
            holder = Instantiate(_minePrefab);
            holder.gameObject.SetActive(false);
            _landMines.Enqueue(holder);

        }
    }


}
