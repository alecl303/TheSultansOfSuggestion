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
            var theta = Mathf.Atan((positionDifference.y - rigidBody.transform.position.y) / (positionDifference.x - rigidBody.transform.position.x)) * (180/Mathf.PI);

            GameObject bullet = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (target.x), rigidBody.transform.position.y + (target.y), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));
            var bulletController = bullet.GetComponent<PlayerBulletController>();

            if(positionDifference.x - rigidBody.transform.position.x < 0)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }

            bulletController.SetTarget(target);
            bulletController.SetDamage(playerObject.GetStats().GetRangeDamage());
            bulletController.SetBulletSpeed(playerObject.GetStats().GetBulletSpeed());
            
            bulletController.SetStunChance(playerObject.GetStats().GetStunChance());
            bulletController.SetStunTime(playerObject.GetStats().GetStunTime());

            bulletController.SetPoisonChance(playerObject.GetStats().GetPoisonChance());
            bulletController.SetPoisonTime(playerObject.GetStats().GetPoisonTime());

        }
    }
}
