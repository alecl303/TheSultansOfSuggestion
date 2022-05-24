using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Berserk : ScriptableObject, IPlayerCommand
    {

        private int requiredMana = 50;
        private float duration = 5;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                playerStats.DrainMana(this.requiredMana);
                playerStats.StartCoroutine(playerStats.BerserkForXSeconds(duration));
            }
        }
    }
}
