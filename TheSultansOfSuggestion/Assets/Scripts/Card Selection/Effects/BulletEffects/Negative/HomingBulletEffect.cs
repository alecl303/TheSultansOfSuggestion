using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;

namespace Player.Effect
{
    public class HomingBulletEffect : ScriptableObject, IPlayerEffect
    {
        /* This is an abomonation. Maybe it gets tossed */

        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            //playerStats.bulletMovement = CreateInstance<HomingBullet>();
        }
        public string GetDescription()
        {
            return "Your bullets will become homing bullets";
        }

        public string GetName()
        {
            return "HomingBullet";
        }
    }
}