using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;
using Player.Command;

namespace Player.Effect
{
    public class SpellsCostBlood : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var playerStats = gameObject.GetComponent<PlayerController>().GetStats();
            playerStats.manaIsHp = true;
        }
        public string GetDescription()
        {
            return "Spells will use hp instead of mana (reduce cost by 80%)";
        }

        public string GetName()
        {
            return "SpellsCostBlood";
        }
    }
}
