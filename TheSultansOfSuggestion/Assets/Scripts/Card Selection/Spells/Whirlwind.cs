using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Whirlwind : ScriptableObject, IPlayerCommand
    {

        private int requiredMana = 50;

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

        // public Sprite ReturnSprite(GameObject gameObject)
        // {
        //     var playerStats = gameObject.GetComponent<PlayerController>().GetStats();
        //     return (playerStats.whirlwind.GetComponent<SpriteMask>().sprite);
        // }
    }
}
