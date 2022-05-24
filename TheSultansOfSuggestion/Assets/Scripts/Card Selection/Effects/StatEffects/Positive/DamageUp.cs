using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    // This is a Test Effect
    public class DamageUp : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "base damage";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.meleeDamage *= (1 + this.changeAmount);
            playerStats.rangeDamage *= (1 + this.changeAmount);
        }
    }
}
