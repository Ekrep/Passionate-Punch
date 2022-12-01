using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMana : MonoBehaviour
    {
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

        void Start()
        {

        }

        void Update()
        {
            if (canRecover && _Character.mana < _Character.maxMana)
            {
                if (isPeriodPassed)
                    _Character.mana += _Character.manaRecoveryAmount;
                lastRecoveredTime = Time.time;
            }

        }
    }
}