using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{

    public interface IHealth
    {
        void DecreaseHealth(float amount);
        void KillSelf();
    }
}
