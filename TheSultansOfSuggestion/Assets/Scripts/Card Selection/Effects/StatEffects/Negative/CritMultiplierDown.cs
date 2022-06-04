using System.Collections;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class CritMultiplierDown : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.critMultiplier -= 0.25f;
        }
        public string GetDescription()
        {
            return "Decrease your crit multiplier by 25%";
        }

        public string GetName()
        {
            return "CritMultiplierUp";
        }
    }
}