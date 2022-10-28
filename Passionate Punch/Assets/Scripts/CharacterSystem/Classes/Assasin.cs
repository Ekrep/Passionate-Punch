using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

namespace Classes
{
    [CreateAssetMenu(menuName = "Scriptables/Classes/Assasin")]
    public class Assasin : CharacterSettings
    {
        public override void DecreaseHealth(float amount)
        {
            throw new System.NotImplementedException();
        }

        public override void KillSelf()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}

