using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;

namespace Player.Effect
{
    public class OrbitBulletEffect : ScriptableObject, IPlayerEffect
    {
        /* This is an abomonation. Maybe it gets tossed */

        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.bulletMovement = CreateInstance<OrbitBullet>();
        }
        public string GetDescription()
        {
            return "Your bullets will do math";
        }

        public string GetName()
        {
            return "OrbitBullet";
        }
    }
}