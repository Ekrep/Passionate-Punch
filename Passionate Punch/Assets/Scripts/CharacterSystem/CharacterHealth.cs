using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace CharacterSystem
{


    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField] CharacterSettings character;
        private float health;
        public float Health { get => health; set => health = value;}
        public float lastDamageTakenTime;
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastDamageTakenTime + character.recoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + character.recoveryPeriod;

        void IHealth.DecreaseHealth(float amount)
        {
            throw new System.NotImplementedException();
        }

        void IHealth.KillSelf()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(canRecover)
            {
                if(isPeriodPassed)
                    this.Health += character.recoveryAmount;
            }
        }
    }
}