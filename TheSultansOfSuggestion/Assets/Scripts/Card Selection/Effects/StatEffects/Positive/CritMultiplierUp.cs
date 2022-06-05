using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class CritMultiplierUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.critMultiplier += 0.4f;
        }
        public string GetDescription()
        {
            return "Increase your crit multiplier by 40%";
        }

        public string GetName()
        {
            return "CritMultiplierUp";
        }
    }
}
