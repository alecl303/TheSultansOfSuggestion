using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class BulletSizeDown : StatEffect
    {
        public override void Init()
        {
            base.Init();
            this.affectedStat = "bullet size";
            this.affliction = "Decrease";
            this.name = "BulletSizeDown";
        }

        public override void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            //playerStats.bulletSize *= 0.9f;
        }

        //public string GetName()
        //{
        //    return "BulletSizeDown";
        //}
    }
}
