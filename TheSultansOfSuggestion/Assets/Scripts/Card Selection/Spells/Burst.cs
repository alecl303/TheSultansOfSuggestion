using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class Burst : ScriptableObject, IPlayerCommand
    {

        private int requiredMana = 25;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();
            var playerStats = player.GetStats();
            var bullet = player.bulletPrefab;

            if (rigidBody != null && playerStats.GetMana() >= this.requiredMana)
            {
                for(int i = 0; i < 360; i += 15)
                {
                    float theta = i * (Mathf.PI /180);
                    var target = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)).normalized;

                    var bulletController = Instantiate(bullet, new Vector3(rigidBody.transform.position.x + Mathf.Cos(theta), rigidBody.transform.position.y + Mathf.Sin(theta), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<PlayerBulletController>();
                    
                    bulletController.SetTarget(target);
                    bulletController.SetDamage(playerStats.GetRangeDamage());
                    bulletController.SetBulletSpeed(playerStats.GetBulletSpeed());
                }

                playerStats.DrainMana(this.requiredMana);
            }
        }
    }
}
