using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class BulletSpeedDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "bullet speed";
            this.affliction = "Decrease";
            this.name = "BulletSpeedDown";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.bulletLifeSpan *= (1 - this.changeAmount);
        }

        //public string GetName()
        //{
        //    return "BulletSpeedDown";
        //}
    }
}

