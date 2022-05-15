using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class RangedAttack : ScriptableObject, IPlayerCommand
    {

        public void Execute(GameObject gameObject)
        {
            var playerObject = gameObject.GetComponent<PlayerController>();

            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var worldTransform = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            var positionDifference = Input.mousePosition - worldTransform;
            var target = positionDifference.normalized;



            GameObject bullet = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (target.x/4), rigidBody.transform.position.y + (target.y/4), rigidBody.transform.position.z), new Quaternion());
            var bulletController = bullet.GetComponent<PlayerBulletController>();

            bulletController.SetTarget(target);
            bulletController.SetBulletDamage(playerObject.GetRangeDamage());
            bulletController.SetBulletSpeed(playerObject.GetBulletSpeed());

        }
    }
}

