using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

namespace CharacterUtilities
{

    public class CharacterExperience : MonoBehaviour
    {

        [SerializeField]
        private CharacterSettings _Character
        {
            get
            {
                return GameManager.Instance.character.characterStats;
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }

        //This function will be called when an enemy is killed by player. 
        public void GainExperience()
        {
            _Character.experience += 10; // Dummy for now

            if(_Character.experience >= _Character.experienceThreshold)
            {
                float temp = _Character.experience - _Character.experienceThreshold;
                _Character.level++;
                _Character.experienceThreshold += 30;
                _Character.experience = temp;
            }
        }
    }
}