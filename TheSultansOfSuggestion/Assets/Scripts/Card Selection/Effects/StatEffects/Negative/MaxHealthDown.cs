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
            this.name = "MaxHealthDown";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.maxHealth *= (1 - this.changeAmount);
        }

        //public string GetName()
        //{
        //    return "MaxHealthDown";
        //}
    }
}
