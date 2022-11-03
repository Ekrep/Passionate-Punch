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
        public float Health { get => health; set => health = value; }
        public float lastDamageTakenTime; //This variable needs to be updated when player gets in a fight.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastDamageTakenTime + character.healthRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + character.healthRecoveryPeriod;

        public void DecreaseHealth(float amount)
        {
            this.Health -= amount;
            if (this.Health <= 0)
                KillSelf();
        }

        public void KillSelf()
        {
            Debug.Log("YOU DIED");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (canRecover && this.Health < character.maxHealth)
            {
                if (isPeriodPassed)
                    this.Health += character.healthRecoveryAmount;
                    lastRecoveredTime = Time.time;
            }
        }
    }
}