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

        public float lastManaUsedTime; //This variable needs to be updated when player uses mana.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastManaUsedTime + _Character.characterStats.manaRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + _Character.characterStats.manaRecoveryPeriod;

         void GameManager_OnSendCharacter(CharacterBaseStateMachine obj){
            _Character = obj;
            
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
            if (canRecover && _Character.characterStats.mana < _Character.characterStats.maxMana)
            {
                if (isPeriodPassed)
                {
                    _Character.characterStats.mana += _Character.characterStats.manaRecoveryAmount;
                    OnManaRecoveryEnabled?.Invoke();
                    lastRecoveredTime = Time.time;

                }
            }

        }
    }
}