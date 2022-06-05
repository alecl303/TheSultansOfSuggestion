using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Heal : ScriptableObject, IPlayerSpell
    {

        private int requiredMana = 100;
        private int duration = 8;
        private float flatHeal = 5.0f; // There is also scaling base on spell strength
        private float cooldown = 14.0f;
        private float timeBeforeHeal = 1.5f;

        public float GetCooldown() 
        {
            return this.cooldown;
        }

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();
            var spellPrefab = playerStats.healCircle;

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                var spellObject = Instantiate(spellPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                spellObject.GetComponent<HealEffect>().SetFlatHeal(this.flatHeal);
                Destroy(spellObject, duration);
                playerStats.DrainMana(this.requiredMana);
            }
        }

        public string GetDescription()
        {
            return "Create a healing circle for " + this.duration + " seconds and heal " + this.flatHeal + " by standing on the circle for " + this.timeBeforeHeal + ". Costs " + this.requiredMana + " mana and has a " + this.cooldown + " cooldown time.";
        }
    }
}
