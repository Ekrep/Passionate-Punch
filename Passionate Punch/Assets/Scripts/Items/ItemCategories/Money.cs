using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace ItemCategories
{
    public class Money : ItemSettings
    {
        public int lowerBound;
        public int upperBound;
        public override void ApplyItemEffect(float amount)
        {
            player.money += (int)amount;
        }
        public override void RevertItemEffect(float amount)
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
    }
}