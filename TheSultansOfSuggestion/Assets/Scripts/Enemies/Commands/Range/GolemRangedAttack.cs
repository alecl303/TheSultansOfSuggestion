using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

namespace Enemy.Command
{
    public class GolemRangedAttack : ScriptableObject, IEnemyCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var enemyObject = gameObject.GetComponent<EnemyController>();
            var target = enemyObject.GetTarget();

            Vector2 positionDifference = target.position - rigidBody.position;

            if (positionDifference.magnitude < enemyObject.GetAttackRange())
            {
                var theta = Mathf.Atan((positionDifference.y) / (positionDifference.x)) * (180 / Mathf.PI);


                GameObject bullet = (GameObject)Instantiate(enemyObject.bulletPrefab, new Vector3(rigidBody.transform.position.x + Mathf.Cos(theta-1), rigidBody.transform.position.y + Mathf.Sin(theta - 1), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta-1));
                GameObject bullet2 = (GameObject)Instantiate(enemyObject.bulletPrefab, new Vector3(rigidBody.transform.position.x + Mathf.Cos(theta), rigidBody.transform.position.y + Mathf.Sin(theta), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));
                GameObject bullet3 = (GameObject)Instantiate(enemyObject.bulletPrefab, new Vector3(rigidBody.transform.position.x + Mathf.Cos(theta + 1), rigidBody.transform.position.y + Mathf.Sin(theta + 1), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta+1));

                if (positionDifference.x < 0)
                {
                    bullet.GetComponent<SpriteRenderer>().flipX = true;
                }

                bullet.GetComponent<BulletController>().SetBulletDamage(enemyObject.GetAttackDamage());
                bullet.GetComponent<BulletController>().SetBulletSpeed(enemyObject.GetBulletSpeed());

                if (positionDifference.x < 0)
                {
                    bullet2.GetComponent<SpriteRenderer>().flipX = true;
                }

                bullet2.GetComponent<BulletController>().SetBulletDamage(enemyObject.GetAttackDamage());
                bullet2.GetComponent<BulletController>().SetBulletSpeed(enemyObject.GetBulletSpeed());

                if (positionDifference.x < 0)
                {
                    bullet3.GetComponent<SpriteRenderer>().flipX = true;
                }

                bullet3.GetComponent<BulletController>().SetBulletDamage(enemyObject.GetAttackDamage());
                bullet3.GetComponent<BulletController>().SetBulletSpeed(enemyObject.GetBulletSpeed());

                enemyObject.StartCoroutine(enemyObject.InitiateAttack());
            }
        }
    }

}
