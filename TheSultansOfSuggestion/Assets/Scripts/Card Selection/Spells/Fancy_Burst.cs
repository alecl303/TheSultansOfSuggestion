using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class FancyBurst : ScriptableObject, IPlayerCommand
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
                for (int i = 0; i < 360; i += 4)
                {
                    float theta = i * (Mathf.PI / 180);

                    var r = Mathf.Sin(1.3f * i);

                    var target = new Vector2((r * Mathf.Cos(theta)),(r * Mathf.Sin(theta)));
                    
                    var x = rigidBody.transform.position.x;
                    var y = rigidBody.transform.position.y;
                    var bulletController = Instantiate(bullet, new Vector3(x, y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<PlayerBulletController>();

                    bulletController.SetTarget(target);
                    bulletController.SetDamage(playerStats.GetRangeDamage());
                    bulletController.SetBulletSpeed(playerStats.GetBulletSpeed());
                }

                playerStats.DrainMana(this.requiredMana);
            }
        }
    }
}
