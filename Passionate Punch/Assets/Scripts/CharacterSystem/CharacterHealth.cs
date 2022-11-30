using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using SkillSystem;

namespace CharacterSystem
{


    public class CharacterHealth : MonoBehaviour, IPlayerHealth
    {
        // This Action (OnPlayerDead) will invoke when player's health is less then 0. Then the killself function can trigger
        private void OnEnable()
        {
            HealthBar.OnPlayerDead += KillSelf;    
        }
        private void OnDisable()
        {
            HealthBar.OnPlayerDead -= KillSelf;
        }
        //////////////////////////////////////////

        private CharacterBaseStateMachine _Character
        {
            get
            {
                return GameManager.Instance.character;
            }
        }
        private float health;
        public float Health { get => health; set => health = value; }
        public float lastDamageTakenTime; //This variable needs to be updated when player gets in a fight.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastDamageTakenTime + _Character.characterStats.healthRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + _Character.characterStats.healthRecoveryPeriod;

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

     
        void Start()
        {

        }

       
        void Update()
        {
            if (canRecover && this.Health < _Character.characterStats.maxHealth)
            {
                if (isPeriodPassed)
                    this.Health += _Character.characterStats.healthRecoveryAmount;
                    lastRecoveredTime = Time.time;
            }
        }

        public void Hit(SkillSettings.HitType hitType, float damage, Vector3 hitPos,float pushAmount)
        {
            switch (hitType)
            {
                case SkillSettings.HitType.Low:
                    DecreaseHealth(damage);
                    break;
                case SkillSettings.HitType.Medium:
                    DecreaseHealth(damage);
                    _Character.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(-hitPos.x * pushAmount * Time.deltaTime, 0,-hitPos.z * pushAmount * Time.deltaTime) , hitPos);
                    break;
                case SkillSettings.HitType.Hard:
                    DecreaseHealth(damage);
                    //character StunState
                    break;
                default:
                    break;
            }
        }
    }
}