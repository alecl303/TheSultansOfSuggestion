using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Whirlwind : ScriptableObject, IPlayerSpell
    {
       
        private int requiredMana = 50;
        private float cooldown = 0.0f;

        public float GetCooldown() {
            
            return this.cooldown;
        }

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var playerStats = gameObject.GetComponent<PlayerController>().GetStats();

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                var whirlwindBox =
                    (GameObject)Instantiate(playerStats.whirlwind,
                        new Vector3(
                            rigidBody.transform.position.x,
                            rigidBody.transform.position.y,
                            rigidBody.transform.position.z),
                        new Quaternion());
                whirlwindBox.GetComponent<PlayerAttack>().SetDamage(playerStats.GetMeleeDamage());
                playerStats.DrainMana(this.requiredMana);
            }
        }

        public string GetDescription()
        {
            return "Deal melee damage to all enemies in a large radius. Costs " + this.requiredMana + " mana.";
        }

        public string GetName()
        {
            return "Whirlwind";
        }
    }
}