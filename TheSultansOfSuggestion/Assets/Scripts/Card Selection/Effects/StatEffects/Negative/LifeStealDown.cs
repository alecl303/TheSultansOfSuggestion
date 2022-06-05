using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class LifeStealDown : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerStats>();
            playerStats.lifeSteal -= 1;
        }
        public string GetDescription()
        {
            return "Decrease your melee life drain by 1 hp. (Values can be negative)";
        }
    }
}
