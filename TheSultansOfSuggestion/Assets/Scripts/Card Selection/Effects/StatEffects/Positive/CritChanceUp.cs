using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class CritChanceUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.critChance += 15;
        }
        public string GetDescription()
        {
            return "Increase your crit chance by 15%";
        }

        public string GetName()
        {
            return "CritChanceUp";
        }
    }
}
