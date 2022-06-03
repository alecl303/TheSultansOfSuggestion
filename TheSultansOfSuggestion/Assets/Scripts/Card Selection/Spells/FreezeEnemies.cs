using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class FreezeEnemies : ScriptableObject, IPlayerSpell
    {

        private int requiredMana = 30;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var playerStats = gameObject.GetComponent<PlayerController>().GetStats();

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                var freezeBox = 
                    (GameObject) Instantiate(playerStats.freezeBox, 
                        new Vector3(
                            rigidBody.transform.position.x, 
                            rigidBody.transform.position.y, 
                            rigidBody.transform.position.z), 
                        new Quaternion());

                playerStats.DrainMana(this.requiredMana);
            }
        }
        public string GetDescription()
        {
            return "Temporarily stun all enemies in a large radius. Costs " + this.requiredMana + " mana.";
        }

        public string GetName()
        {
            return "Freeze";
        }
    }
}
