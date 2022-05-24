using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class RangeDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "bullet range";
            this.affliction = "Decrease";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.bulletLifeSpan *= (1 - this.changeAmount);
        }
    }
}
