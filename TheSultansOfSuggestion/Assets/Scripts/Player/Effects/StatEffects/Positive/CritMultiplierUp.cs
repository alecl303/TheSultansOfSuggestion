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
            playerStats.critMultiplier *= 1.5f;
        }
        public string GetDescription()
        {
            return "Increase your crit chance by 10%";
        }
    }
}
