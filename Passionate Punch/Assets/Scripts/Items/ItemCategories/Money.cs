using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using CharacterSystem;

namespace ItemCategories
{
    public class Money : ItemSettings
    {
        public int lowerBound;
        public int upperBound;
        public override void ApplyItemEffect(CharacterSettings player, float amount)
        {
            player.money += (int)amount;
        }
        public override void RevertItemEffect(CharacterSettings player, float amount)
        {
            player.money -= (int)amount;
        }

        void Start()
        {
            int amount = Random.Range(lowerBound, upperBound);
            this.worth = amount;
        }

        void Update()
        {

        }

       public override void ConfigureDescription()
        {
            itemDescription = "";
        }
    }
}
