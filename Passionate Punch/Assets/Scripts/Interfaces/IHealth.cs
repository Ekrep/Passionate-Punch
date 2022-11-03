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
    }
}
