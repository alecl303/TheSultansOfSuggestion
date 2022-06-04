using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class MaxHealthUp : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "max health";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            var statIncrease = playerStats.maxHealth * (1 + this.changeAmount);
            var healAmount = statIncrease - playerStats.maxHealth;
            playerStats.SetMaxHealth(statIncrease);
            playerStats.Heal(healAmount);
        }
    }
}
