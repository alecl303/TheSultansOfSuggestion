using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class BulletSizeUp : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "bullet size";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            //playerStats.bulletSize *= 1.1f;
        }
    }
}
