using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class IcicleTrap : ScriptableObject, IPlayerSpell
    {

        private int requiredMana = 100;
        private int duration = 50;
        private float cooldown = 8.0f;

        public float GetCooldown() 
        {
            return this.cooldown;
        }
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();
            var spellPrefab = playerStats.icicleTrap;

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                var spellObject = Instantiate(spellPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                spellObject.GetComponent<IcicleEffect>().SetBulletDamage(playerStats.GetRangeDamage());

                Destroy(spellObject, duration);
                playerStats.DrainMana(this.requiredMana);
            }
        }
        public string GetDescription()
        {
            return "Create a trap spell on the floor for " + this.duration + " seconds. Enemies that step on the trap activates a barrage of bullets. Costs " + this.requiredMana + " mana and has a " + this.cooldown + "s cooldown time.";
        }

        public string GetName()
        {
            return "IcicleTrap";
        }
    }
}
