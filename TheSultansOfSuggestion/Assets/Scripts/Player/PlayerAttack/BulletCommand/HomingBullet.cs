using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Command
{
    public class HomingBullet : ScriptableObject, IBulletMovement
    {
        public void Execute(GameObject gameObject)
        {
            var bulletController = gameObject.GetComponent<PlayerBulletController>();
            var target = (FindObjectOfType<EnemyController>().GetComponent<Rigidbody2D>().position - gameObject.GetComponent<Rigidbody2D>().position).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = bulletController.GetBulletSpeed() * target;
        }
    }
}