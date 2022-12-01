using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CharacterSystem
{
    public class CharacterMana : MonoBehaviour
    {
        public static event Action OnManaRecoveryEnabled;

        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }

        public float lastManaUsedTime; //This variable needs to be updated when player uses mana.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastManaUsedTime + _Character.manaRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + _Character.manaRecoveryPeriod;

        void OnEnable()
        {
            GameManager.OnSkillCasted += UpdateLastManaTime;
        }

        void UpdateLastManaTime(float amount)
        {
            lastManaUsedTime = Time.time;
        }

        void OnDisable()
        {
            GameManager.OnSkillCasted -= UpdateLastManaTime;
        }

        void Start()
        {

        }

        void Update()
        {
            if (canRecover && _Character.mana < _Character.maxMana)
            {
                if (isPeriodPassed)
                {
                    _Character.mana += _Character.manaRecoveryAmount;
                    OnManaRecoveryEnabled?.Invoke();
                    lastRecoveredTime = Time.time;

                }
            }

        }
    }
}