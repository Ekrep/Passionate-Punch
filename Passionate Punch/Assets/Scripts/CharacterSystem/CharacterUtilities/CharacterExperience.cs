using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using System;

namespace CharacterUtilities
{

    public class CharacterExperience : MonoBehaviour
    {
        private float lastGainedTime;
        [SerializeField] private float experienceIncreaseTime;
        private bool isPeriodPassed => Time.time > lastGainedTime + experienceIncreaseTime;

        public static event Action onGainExperience;

        [SerializeField]
        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }

        void OnEnable()
        {
            EnemyDieState.OnEnemyDie += GainExperience;
        }

        void OnDisable()
        {
            EnemyDieState.OnEnemyDie -= GainExperience;
        }

        void Start()
        {

        }

        void Update()
        {
            if(isPeriodPassed && _Character.mana < _Character.maxMana)
            {
                onGainExperience?.Invoke();
                lastGainedTime = Time.time;
            }
        }

        //This function will be called when an enemy is killed by player. 
        public void GainExperience()
        {
            _Character.experience += 10; // Dummy for now

            if (_Character.experience >= _Character.experienceThreshold)
            {
                float temp = _Character.experience - _Character.experienceThreshold;
                _Character.level++;
                _Character.experienceThreshold += 30;
                _Character.experience = temp;
            }
        }
    }
}