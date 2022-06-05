using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class MovementSpeedDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "movement speed";
            this.affliction = "Decrease";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.movementSpeed *= (1 - this.changeAmount);
        }
    }
}
