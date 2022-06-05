using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Invincibility : ScriptableObject, IPlayerSpell
    {

        private int requiredMana = 50;
        private float duration = 5;
        private float cooldown = 10.0f;

        public float GetCooldown() 
        {
            return this.cooldown;
        }

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                playerStats.DrainMana(this.requiredMana);
                player.StartCoroutine(player.InvincibleForXSeconds(duration));
            }
        }

        public string GetDescription()
        {
            return "Gain temporary invincibility for " + this.duration + " seconds. Costs " + this.requiredMana + " mana.";
        }
    }
}
