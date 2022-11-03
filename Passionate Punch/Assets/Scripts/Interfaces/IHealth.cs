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

        void DecreaseHealth(float amount);
        void KillSelf();
    }
}
