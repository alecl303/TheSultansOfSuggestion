using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;

namespace Player.Effect
{
    public class BackwardsBulletEffect : ScriptableObject, IPlayerEffect
    {
        /* This is an abomonation. Maybe it gets tossed */

        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.bulletMovement = CreateInstance<BackwardsBullet>();
        }
        public string GetDescription()
        {
            return "Your bullets will fly in reverse";
        }
    }
}