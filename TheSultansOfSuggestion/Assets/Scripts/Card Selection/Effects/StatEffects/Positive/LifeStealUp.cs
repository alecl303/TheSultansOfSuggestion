using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class LifeStealUp : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.lifeSteal += 1;
        }
        public string GetDescription()
        {
            return "Increase your melee lifesteal by 1 hp";
        }

        public string GetName()
        {
            return "LifestealUp";
        }
    }
}
