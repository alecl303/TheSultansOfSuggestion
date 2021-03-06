using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class MovementSpeedUp : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "movement speed";
            this.name = "MovementSpeedUp";
            this.changeAmount = Random.Range(0.1f, 0.2f);
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.movementSpeed *= (1 + this.changeAmount);
        }
    }
}
