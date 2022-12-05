using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CharacterSystem
{
    public class CharacterMana : MonoBehaviour
    {
        public static event Action OnManaRecoveryEnabled;
        private CharacterBaseStateMachine _Character;
        private float manaRecoveryAmount;
        private float manaRecoveryPeriod;
        private float manaRecoveryTime;
        private float maxMana;
        private float mana;
        public float lastManaUsedTime; //This variable needs to be updated when player uses mana.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastManaUsedTime + manaRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + manaRecoveryPeriod;

         void GameManager_OnSendCharacter(CharacterBaseStateMachine obj){
            _Character = obj;
            manaRecoveryTime = _Character.characterStats.manaRecoveryTime;
            manaRecoveryPeriod = _Character.characterStats.manaRecoveryPeriod;
            manaRecoveryAmount = _Character.characterStats.manaRecoveryAmount;
            maxMana = _Character.characterStats.maxMana;
            mana = _Character.characterStats.mana;

            
        }


        void OnEnable()
        {
            GameManager.OnSkillCasted += UpdateLastManaTime;
            GameManager.OnSendCharacter += GameManager_OnSendCharacter;
        }

        void UpdateLastManaTime(float amount)
        {
            lastManaUsedTime = Time.time;
        }

        void OnDisable()
        {
            GameManager.OnSkillCasted -= UpdateLastManaTime;
            GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
        }

        void Start()
        {

        }

        void Update()
        {
            if (canRecover && mana < maxMana)
            {
                if (isPeriodPassed)
                {
                    mana += manaRecoveryAmount;
                    OnManaRecoveryEnabled?.Invoke();
                    lastRecoveredTime = Time.time;

                }
            }

        }
    }
}