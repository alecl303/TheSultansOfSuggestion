using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class IncreaseEliteChance : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.eliteChance += 10;
        }
        public string GetDescription()
        {
            return "Increase your chance to spawn an elite by 10%";
        }

        public string GetName()
        {
            return "IncreaseEliteChance";
        }
    }
}
