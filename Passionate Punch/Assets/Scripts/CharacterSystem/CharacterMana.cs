using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMana : MonoBehaviour
    {
        [SerializeField] CharacterSettings character;

        public float lastManaUsedTime; //This variable needs to be updated when player uses mana.
        public float lastRecoveredTime;
        public bool canRecover => Time.time >= lastManaUsedTime + character.manaRecoveryTime;
        public bool isPeriodPassed => Time.time > lastRecoveredTime + character.manaRecoveryPeriod;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (canRecover && character.mana < character.maxMana)
            {
                if (isPeriodPassed)
                    character.mana += character.manaRecoveryAmount;
                lastRecoveredTime = Time.time;
            }

        }
    }
}