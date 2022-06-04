using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class StunChanceUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.stunChance += 5;
        }
        public string GetDescription()
        {
            return "Increase your stun chance by 5%";
        }

        public string GetName()
        {
            return "StunChanceUp";
        }
    }
}
