using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Command
{
    public class StandardBullet : ScriptableObject, IBulletMovement
    {
        public void Execute(GameObject gameObject)
        {
            var bulletController = gameObject.GetComponent<PlayerBulletController>();
            gameObject.GetComponent<Rigidbody2D>().velocity = bulletController.GetBulletSpeed() * bulletController.GetTarget();
        }
    }
}