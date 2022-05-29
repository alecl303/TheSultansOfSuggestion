using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class icicleTrap : ScriptableObject, IPlayerCommand
    {

        private int requiredMana = 75;
        private int duration = 20;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();
            var spellPrefab = playerStats.icicleTrap;

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                var spellObject = Instantiate(spellPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                
                Destroy(spellObject, duration);
                playerStats.DrainMana(this.requiredMana);
            }
        }
    }
}
