using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using SkillSystem;
using System;

namespace CharacterSystem
{


    public class CharacterHealth : MonoBehaviour, IPlayerHealth
    {
        public static event Action<float> OnTakeDamage;
        public static event Action<float> OnHealthRecovery;
        public static event Action OnPlayerDeath;

        private CharacterBaseStateMachine _Character;
        private float healthRecoveryTime;
        private float healthRecoveryPeriod;
        private float maxHealth;
        private float healthRecoveryAmount;
        // This Action (OnPlayerDead) will invoke when player's health is less then 0. Then the killself function can trigger
        private void OnEnable()
        {
            HealthBar.OnPlayerDead += KillSelf;
            GameManager.OnSendCharacter += GameManager_OnSendCharacter;
            Respawn.OnRespawn += RespawnPlayer;
        }
        private void OnDisable()
        {
            HealthBar.OnPlayerDead -= KillSelf;
            GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
            Respawn.OnRespawn -= RespawnPlayer;
        }
        //////////////////////////////////////////


         void GameManager_OnSendCharacter(CharacterBaseStateMachine obj){
            _Character = obj;
            healthRecoveryTime = _Character.characterStats.healthRecoveryTime;
            healthRecoveryPeriod = _Character.characterStats.healthRecoveryPeriod;
            maxHealth = _Character.characterStats.maxHealth;
            healthRecoveryAmount = _Character.characterStats.healthRecoveryAmount;

        }

        private float health = 100f; // For the test purposes only. 
        public float Health { get => health; set => health = value; }
        public float lastDamageTakenTime; //This variable needs to be updated when player gets in a fight.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastDamageTakenTime + healthRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + healthRecoveryPeriod;

        public void DecreaseHealth(float amount)
        {
            lastDamageTakenTime = Time.time;
            this.Health -= amount;
            OnTakeDamage?.Invoke(this.Health);
            if (this.Health <= 0) { }
                //KillSelf();
        }

        public void KillSelf()
        {
            OnPlayerDeath?.Invoke();
            Debug.Log("YOU DIED");
        }


        void Start()
        {

        }

        void Update()
        {
            if (canRecover && this.Health < maxHealth)
            {
                if (isPeriodPassed)
                {
                    this.Health += healthRecoveryAmount;
                    lastRecoveredTime = Time.time;
                    OnHealthRecovery?.Invoke(this.Health);
                }
            }
        }

        public void RespawnPlayer()
        {
            this.Health = _Character.characterStats.maxHealth;
        }
        
        public void Hit(SkillSettings.HitType hitType, float damage, Vector3 hitPos, float pushAmount)
        {
            switch (hitType)
            {
                case SkillSettings.HitType.Low:
                    DecreaseHealth(damage);
                    break;
                case SkillSettings.HitType.Medium:
                    DecreaseHealth(damage);
                    _Character.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(-hitPos.x * pushAmount * Time.deltaTime, 0, -hitPos.z * pushAmount * Time.deltaTime), hitPos);
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