using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class TripleShotRangedAttack : ScriptableObject, IPlayerCommand
    {

        public void Execute(GameObject gameObject)
        {
            var playerObject = gameObject.GetComponent<PlayerController>();
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var worldTransform = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            var positionDifference = Input.mousePosition - worldTransform;
            var target = positionDifference.normalized;
            var theta = Mathf.Atan((positionDifference.y - rigidBody.transform.position.y) / (positionDifference.x - rigidBody.transform.position.x)) * (180 / Mathf.PI);

            GameObject bullet1 = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (target.x / 4) + (0.2f * Mathf.Cos(theta + 2)), rigidBody.transform.position.y + (target.y / 4) + (0.1f * Mathf.Sin(theta + 2)), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));
            GameObject bullet2 = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (target.x) + (0.2f * Mathf.Cos(theta - 2)), rigidBody.transform.position.y + (target.y / 4) + (0.1f * Mathf.Sin(theta - 2)), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));
            GameObject bullet3 = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (target.x / 4), rigidBody.transform.position.y + (target.y / 4), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));

            var bulletController1 = bullet1.GetComponent<PlayerBulletController>();
            var bulletController2 = bullet2.GetComponent<PlayerBulletController>();
            var bulletController3 = bullet3.GetComponent<PlayerBulletController>();

            if (positionDifference.x - rigidBody.transform.position.x < 0)
            {
                bullet1.GetComponent<SpriteRenderer>().flipX = true;
                bullet2.GetComponent<SpriteRenderer>().flipX = true;
                bullet3.GetComponent<SpriteRenderer>().flipX = true;
            }

            List<PlayerBulletController> bullets = new List<PlayerBulletController>
            {
                bulletController1,
                bulletController2,
                bulletController3
            };

            foreach (var bullet in bullets)
            {
                bullet.SetTarget(target);
                bullet.SetDamage(playerObject.GetStats().GetRangeDamage());
                bullet.SetBulletSpeed(playerObject.GetStats().GetBulletSpeed());

                bullet.SetStunChance(playerObject.GetStats().GetStunChance());
                bullet.SetStunTime(playerObject.GetStats().GetStunTime());

                bullet.SetPoisonChance(playerObject.GetStats().GetPoisonChance());
                bullet.SetPoisonTime(playerObject.GetStats().GetPoisonTime());
            }
        }
    }
}
