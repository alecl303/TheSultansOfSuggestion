using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class MaxHealthDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "max health";
            this.affliction = "Decrease";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            var newMaxHealth = playerStats.maxHealth * (1 - this.changeAmount);
            playerStats.SetMaxHealth(newMaxHealth);
        }
    }
}
