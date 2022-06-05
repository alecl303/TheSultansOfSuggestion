using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class PoisonTimeUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.poisonTime += 1;
            playerStats.poisonChance += 3;
        }
        public string GetDescription()
        {
            return "Increase your poison duration by 1 second, and poison chance by 3%";
        }

        public string GetName()
        {
            return "PoisonTimeUp";
        }
    }
}