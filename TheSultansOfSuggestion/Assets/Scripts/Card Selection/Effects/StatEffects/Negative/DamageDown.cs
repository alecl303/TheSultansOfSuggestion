using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    // This is a Test Effect
    public class DamageDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "base damage";
            this.affliction = "Decrease";
            this.name = "DamageDown";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.meleeDamage *= (1 - this.changeAmount);
            playerStats.rangeDamage *= (1 - this.changeAmount);
        }

        public string GetName()
        {
            return "DamageDown";
        }
    }
}
