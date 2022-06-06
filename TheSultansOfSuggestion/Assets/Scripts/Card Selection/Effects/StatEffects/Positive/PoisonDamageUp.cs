using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class PoisonDamageUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.poisonTickDamage += 1;
            playerStats.poisonChance += 3;
        }
        public string GetDescription()
        {
            return "Increase your poison tick damage by 1, and poison chance by 3%";
        }

        public string GetName()
        {
            return "PoisonDamageUp";
        }
    }
}