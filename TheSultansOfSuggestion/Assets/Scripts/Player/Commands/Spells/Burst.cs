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
            var bullet = player.bulletPrefab;

            if (rigidBody != null && player.GetMana() >= this.requiredMana)
            {
                for(int i = 0; i < 360; i += 15)
                {
                    float theta = i / (Mathf.PI * 2);

                    var target = new Vector2(rigidBody.transform.position.x + 5*Mathf.Cos(theta), rigidBody.transform.position.y + 5*Mathf.Sin(theta)).normalized;

                    var bulletController = Instantiate(bullet, new Vector3(rigidBody.transform.position.x + Mathf.Cos(theta), rigidBody.transform.position.y + Mathf.Sin(theta), rigidBody.transform.position.z), new Quaternion()).gameObject.GetComponent<PlayerBulletController>();

                    bulletController.SetTarget(target);
                    bulletController.SetBulletDamage(player.GetRangeDamage());
                    bulletController.SetBulletSpeed(player.GetBulletSpeed());
                }
            }

            player.DrainMana(this.requiredMana);
        }
    }
}
