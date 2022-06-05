using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class CritChanceDown : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.critChance -= 1;
        }
        public string GetDescription()
        {
            return "Decrease your crit chance by 1%";
        }
    }
}
