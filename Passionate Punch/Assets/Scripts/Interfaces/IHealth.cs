using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{

    public interface IHealth
    {
        float Health
        {
            get;
            set;
        }

        public void DecreaseHealth(float amount);
        public void KillSelf();

        public void Hit(SkillSystem.SkillSettings.HitType hitType, float damage, Vector3 hitPos, float pushAmount);
    }

    public interface IPlayerHealth
    {
        float Health
        {
            get;
            set;
        }

        public void DecreaseHealth(float amount);
        public void KillSelf();

        public void Hit(SkillSystem.SkillSettings.HitType hitType, float damage, Vector3 hitPos, float pushAmount);
    }
}
